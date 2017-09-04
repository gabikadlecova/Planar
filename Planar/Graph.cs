using System;
using System.Collections.Generic;
using Planar.Interfaces;

namespace Planar
{
    public class Graph :IGraph
    {
        public Graph()
        {
            Edges = new List<IEdge>();
            Nodes = new List<INode>();
            _dfsStack = new Stack<INode>();
            SpanningTreeEdges = new List<IEdge>();
            _span = false;
        }
        public List<IEdge> Edges { get; }

        public List<INode> Nodes { get; }

        public List<IEdge> SpanningTreeEdges { get; }
        public int ComponentCount { get; private set; }

        private Stack<INode> _dfsStack;

        

        public void SetComponents()
        {
            _span = true;
            ComponentCount = 0;
            foreach (INode node in Nodes)
            {
                if (!((Node) node).Visited)
                {
                    ComponentCount++;
                    IVisitor visitor = new ComponentVisitor(ComponentCount);
                    DepthFirstSearch(node,visitor);
                }
            }
        }

        public void SetTransitiveClosure()
        {
            _span = false;
            foreach (INode node in Nodes)
            {
                foreach (INode secondNode in Nodes)
                {
                    ((Node)secondNode).Visited = false;
                }
                IVisitor visitor = new TransitiveVisitor(node);
                DepthFirstSearch(node, visitor);
            }
        }

        private bool _span;

        private void DepthFirstSearch(INode startNode, IVisitor visitor)
        {
            _dfsStack.Push(startNode);
            visitor.Visit(startNode);
            ((Node) startNode).Visited = true;
            foreach (IEdge edge in startNode.Edges)
            {
                INode to = edge.To;
                if (!((Node)to).Visited && !_dfsStack.Contains(to))
                {
                    visitor.Visit(to);
                    if (_span)
                        SpanningTreeEdges.Add(edge);
                    ((Node)to).Visited = true;
                    DepthFirstSearch(to, visitor);
                }
            }
            _dfsStack.Pop();

        }

       

        public void SetMinSpanningTree()
        {
            Edges.Sort(Edge.Comparison);
            //ToDo if time later
        }
        

    }
}