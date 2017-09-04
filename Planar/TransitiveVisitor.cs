using Planar.Interfaces;

namespace Planar
{
    public class TransitiveVisitor : IVisitor
    {
        private readonly Node _startNode;
        public TransitiveVisitor(INode startNode)
        {
            _startNode = (Node)startNode;
        }
        public void Visit(INode node)
        {
            IEdge edge = new Edge(_startNode,node);
            _startNode.TransitiveClosureEdges.Add(edge);
        }
    }
}