using System.Collections.Generic;

namespace Planar.Interfaces
{
    public interface IGraph
    {
        List<INode> Nodes { get; }
        List<IEdge> Edges { get; }
    }
}