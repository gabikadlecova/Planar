using System.Collections.Generic;
using System.Drawing;

namespace Planar.Interfaces
{
    public interface INode
    {
        Position Position { get; set; }
        Point ToPoint();
        int Index { get; }

        List<IEdge> Edges { get; }
        IForce CurrentForce { get; set; }
    }
}