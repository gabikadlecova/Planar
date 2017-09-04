using System;
using System.Linq;
using Planar.Interfaces;

namespace Planar
{
    public class Edge :IEdge, IComparable<IEdge>
    {
        public readonly INode[] Nodes;

        public Edge(INode from, INode to)
        {
            Nodes = new[] {from, to};
        }
        public INode From
        {
            get
            {
                return Nodes[0]; 
            }
        }
        public INode To {
            get
            {
                return Nodes[1];
            }
        }

        public bool InSpanningTree { get; set; }

        public INode GetOtherNode(INode firstNode)
        {
            if (firstNode.Equals(Nodes[0])) return Nodes[1]; 
            if (firstNode.Equals(Nodes[1])) return Nodes[0];
            return null;
        }

        public double GetLength()
        {
            return To.Position.GetDistance(From.Position);
        }

        public static int Comparison(IEdge e1, IEdge e2)
        {
            return ((Edge)e1).CompareTo(e2);
        }
        public int CompareTo(IEdge other)
        {
            double firstLength = GetLength();
            double secondLength = other.GetLength();
            return firstLength.CompareTo(secondLength);
        }
    }
}