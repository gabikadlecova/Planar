using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Planar.Interfaces;

namespace Planar
{
    public class GraphDrawer : IGraphDrawer
    {
        private const int GraphDimension = 2;

        private const double Range = 0.00001D; 
        private const double SpringChar = 0.1D;   //Hooke's law constant
        private const double CustomCoulomb = 10000D; //Coulomb's constant-like
        private const double MaxEdgeLength = 100D;
        private const double SlowDown =0.8D;
        private const Int64 IterationLimit = 6000;
        private const int TresholdCount = 20;

        private Position[] _dimensions = new Position[2];
        private double Width
        {
            get
            {
                return _dimensions[1][0] - _dimensions[0][0];
            }
        }
        private double Height
        {
            get
            {
                return _dimensions[1][1] - _dimensions[0][1];
            }
        }
        

        private void SetDimensions()
        {
            double minX = Graph.Nodes[0].Position[0];
            double minY = Graph.Nodes[0].Position[1];
            double maxX = 0;
            double maxY = 0;
            foreach (INode node in Graph.Nodes)
            {
                if (node.Position[0] < minX) minX = node.Position[0];
                if (node.Position[1] < minY) minY = node.Position[1];

                if (node.Position[0] > maxX) maxX = node.Position[0];
                if (node.Position[1] > maxY) maxY = node.Position[1];
            }
            _dimensions[0] = new Position(minX, minY);
            _dimensions[1] = new Position(maxX, maxY);
        }
        public GraphDrawer(Graph graph)
        { 
            Graph = graph;
            Labels = new List<Label>();
        }

        private void Resize(double minX, double minY, double maxX, double maxY )
        {
            SetDimensions();
            double scaleX = (maxX - minX) / Width;
            double scaleY = (maxY - minY) / Height;
            

            foreach (INode node in Graph.Nodes)
            {
                double xOffSet = node.Position[0] - _dimensions[0][0];
                double yOffSet = node.Position[1] - _dimensions[0][1];
                xOffSet *= scaleX;
                yOffSet *= scaleY;
                node.Position[0] = minX + xOffSet;
                node.Position[1] = minY + yOffSet;
               

            }

            _dimensions[0][0] = minX;
            _dimensions[0][1] = minY;
            _dimensions[1][0] = maxX;
            _dimensions[1][1] = maxY;
        }

        private Rectangle GetNodeRectangle(INode node, double size)
        {
            double uppX = node.Position[0] - size / 2;
            double uppY = node.Position[1] - size / 2;
            return new Rectangle((int)uppX, (int)uppY, (int)size, (int)size);
        }

        public void Draw(Graphics g, Rectangle drawingPlace, int nodeSize, int lineSize,Color lineColor, Color nodeColor)
        {

            Resize(drawingPlace.X, drawingPlace.Y, drawingPlace.Right, drawingPlace.Bottom);
            foreach (IEdge edge in Graph.Edges)
            {
                Point first = edge.From.ToPoint();
                Point second = edge.To.ToPoint();
                using (Pen linePen = new Pen(lineColor, (int)lineSize))
                {
                    g.DrawLine(linePen, first, second);
                }
            }
            foreach (INode node in Graph.Nodes)
            {
                using (Brush nodeBrush = new SolidBrush(nodeColor))
                {
                    Rectangle circleRectangle = GetNodeRectangle(node, nodeSize);
                    g.FillEllipse(nodeBrush, circleRectangle);
                }
            }
            for (int i = 1; i <= ((Graph)Graph).ComponentCount; i++)
            {
                SetComponentLabel(i);
            }
        }

        public Graph GetTransitiveClosure()
        {
            Graph graph = new Graph();
            foreach (INode node in Graph.Nodes)
            {
                graph.Nodes.Add(node);
            }
            foreach (INode node in graph.Nodes)
            {
                foreach (IEdge edge in ((Node)node).TransitiveClosureEdges)
                {
                    graph.Edges.Add(edge);
                }
            }
            return graph;
        }

        public void SetComponentLabel(int componentNumber)
        {
            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;
            foreach (INode node in Graph.Nodes)
            {
                if (((Node) node).ComponentNumber == componentNumber)
                {
                    if (node.Position[1] < minY)
                        minY = (int) node.Position[1];
                    if (node.Position[0] < minX)
                        minX = (int)node.Position[0];
                }
            }
            if (minY != Int32.MaxValue)
            {
                Label componentLabel = new Label();
                componentLabel.Text = String.Format("Component {0}", componentNumber);
                componentLabel.AutoSize = true;
                componentLabel.TabIndex = 1;
                componentLabel.Location = new Point(minX+5, minY-28);
                componentLabel.Size = new System.Drawing.Size(100, 24);
                componentLabel.Name = String.Format("componentLabel{0}", componentNumber);
                componentLabel.Visible = false;
                Labels.Add(componentLabel);
                
            }
        }

        public List<Label> Labels;


        public IGraph Graph { get; }

        private void ApplyForce(INode node, IForce force)
        {
            node.Position.MoveByVector(force);
        }

        public void SetGraph() //ToDo components
        {
            ((Graph)Graph).SetComponents();
            ((Graph)Graph).SetTransitiveClosure();

            int limit = 0;
            int tresholdCounter = 0;
            bool treshold = false;
            

            while (limit <= IterationLimit && tresholdCounter <= TresholdCount) //overRange)    // repeated when at least one force was greater then range
            {
                treshold = true;
                limit++;

                foreach (INode node in Graph.Nodes)
                {
                    node.CurrentForce = new Force(Position.GetNullVector(GraphDimension));
                }

                foreach (INode node in Graph.Nodes)        // creating and applying forces
                {
                    
                    foreach (INode secondNode in Graph.Nodes)          //checking distances to other nodes
                    {
                        if (!node.Equals(secondNode))       //only other nodes
                        {
                            double distance = node.Position.GetDistance(secondNode.Position);
                            
                                Position pos =
                                    node.Position - secondNode.Position; //creating the force vector coordinates

                                IForce tempForce = new Force(pos); //this force pushes a node away from the other one
                                double forceSize = CustomCoulomb / Math.Pow(distance, 2); //scaling 
                                if (forceSize > 0.0005)
                                    tempForce.Resize(forceSize);

                                node.CurrentForce = (Force) node.CurrentForce + (Force) tempForce; //adding forces

                        }
                    }

                    foreach (IEdge edge in node.Edges)  //checking the length of edges
                    {

                        double currEdgeLength = edge.GetLength();
                        if (currEdgeLength > MaxEdgeLength)     //too long edges
                        {
                            INode otherNode = ((Edge) edge).GetOtherNode(node);
                            Position pos = node.Position - otherNode.Position; //as this graph is not oriented, a method is provided to get the other node

                            IForce tempForce = new Force(pos);  //this force pushes the node towards the second one in order to shorten the edge
                            double forceSize = Math.Pow(currEdgeLength - MaxEdgeLength,1) * SpringChar;
                            if (forceSize > 0.0005)
                                tempForce.Resize(forceSize);

                             otherNode.CurrentForce = (Force)otherNode.CurrentForce + (Force) tempForce;
                        }
                    
                    }
                    
                }
                foreach (INode node in Graph.Nodes)
                {


                    double size = node.CurrentForce.GetLength();
                    if (size >0.0005)
                        node.CurrentForce.Resize(size * SlowDown);
                    size = size * SlowDown;
                    if (size > Range) //the cycle is repeated until all forces remain small
                        treshold = false;
                    ApplyForce(node, node.CurrentForce);
                    
                }
                if (treshold)
                    tresholdCounter++;
            }
        }
    }
}