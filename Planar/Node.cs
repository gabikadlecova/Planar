using System;
using System.Collections.Generic;
using System.Drawing;
using Planar.Interfaces;

namespace Planar
{
    public class Node :INode
    {
        public int ComponentNumber;
        public bool Visited;

        public Node(int index, Position startPosition)
        {
            Edges = new List<IEdge>();
            TransitiveClosureEdges = new List<IEdge>();
            Position = startPosition;
            Index = index;
            CurrentForce = new Force(Position.GetNullVector(startPosition.Dimension));
            ComponentNumber = -1;
            Visited = false;
        }

        public Point ToPoint()
        {
            if (Position.Dimension != 2)
                throw new FormatException("Node dimension must be 2");
            return new Point((int)Position[0], (int)Position[1]);
        }

        
        public Position Position { get; set; }
        public int Index { get; }


        
        public List<IEdge> TransitiveClosureEdges { get; }
        public List<IEdge> Edges { get; }
        public IForce CurrentForce { get; set; }
    }
}