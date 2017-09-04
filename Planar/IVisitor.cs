using Planar.Interfaces;

namespace Planar
{
    public interface IVisitor
    {
        void Visit(INode node);
    }
}