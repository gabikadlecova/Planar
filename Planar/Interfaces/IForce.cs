namespace Planar.Interfaces
{
    public interface IForce
    {
        Position VectorPosition { get; set; }
        void Resize(double size);
        double GetLength();
    }
}