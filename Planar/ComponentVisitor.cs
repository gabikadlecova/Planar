using Planar.Interfaces;

namespace Planar
{
    public class ComponentVisitor : IVisitor
    {
        public int CurrentComponentNumber;

        public ComponentVisitor(int startNumber)
        {
            CurrentComponentNumber = startNumber;
        }
        public void Visit(INode node)
        {
            ((Node) node).ComponentNumber = CurrentComponentNumber;
        }
    }
}