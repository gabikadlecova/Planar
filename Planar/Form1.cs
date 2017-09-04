using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Planar.Interfaces;

namespace Planar
{
    public partial class Form1 : Form
    {
        private Graph _myGraph;
        private List<Label> _labels;
        private Graph _transitiveClosure;
        private Graph _spanningTree;

        public Form1()
        {
            InitializeComponent();
            _labels = new List<Label>();
        }

        private void InitializeGraph()
        {
            GraphReader reader = new GraphReader();
            _myGraph = (Graph)reader.GetGraph();
            foreach (INode node in _myGraph.Nodes)
            {
                node.Position[0] = r.Next(ClientSize.Width);
                node.Position[1] = r.Next(ClientSize.Height);
            }
            
        }

        private readonly Random r = new Random();

        private void DrawPlanarGraph()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(Color.Cornsilk);
                GraphDrawer drawer = new GraphDrawer(_myGraph);
                Rectangle drawingPlace = new Rectangle(50, 50, ClientSize.Width-100, panel1.Top-100);
            
                drawer.SetGraph();
                drawer.Draw(g, drawingPlace, 10, 5, Color.Firebrick, Color.RoyalBlue);
                foreach (Label label in drawer.Labels)
                {
                    
                    this.Controls.Add(label);
                    _labels.Add(label);
                }

                _transitiveClosure = drawer.GetTransitiveClosure();
                _spanningTree = new Graph();
                foreach (INode node in _myGraph.Nodes)
                {
                    _spanningTree.Nodes.Add(node);
                }

                foreach (IEdge edge in _myGraph.SpanningTreeEdges)
                {
                    _spanningTree.Edges.Add(edge);
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            InitializeGraph();
            DrawPlanarGraph();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            DrawPlanarGraph();
        }

        private void componentButton_Click(object sender, EventArgs e)
        {
            foreach (Label label in _labels)
            {
                label.Visible = !label.Visible;
            }
        }

        private void transitButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics()) //toDo redo if time later
            {
                GraphDrawer drawer = new GraphDrawer(_transitiveClosure);
                Rectangle drawingPlace = new Rectangle(50, 50, ClientSize.Width - 100, panel1.Top - 100); 
                
                drawer.Draw(g, drawingPlace, 10, 5, Color.ForestGreen, Color.RoyalBlue);

            }
        }

        private void spanButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())  //toDo redo better if time later
            {
                GraphDrawer drawer = new GraphDrawer(_spanningTree);
                Rectangle drawingPlace = new Rectangle(50, 50, ClientSize.Width - 100, panel1.Top - 100);

                drawer.Draw(g, drawingPlace, 10, 5, Color.Orchid, Color.RoyalBlue);

            }
        }
    }
}
