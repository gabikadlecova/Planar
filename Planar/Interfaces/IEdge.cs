namespace Planar.Interfaces
{
    public interface IEdge
    {

        INode From { get; }
        INode To { get; }
        double GetLength();
    }
}