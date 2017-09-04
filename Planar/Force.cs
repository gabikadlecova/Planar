using System;
using Planar.Interfaces;

namespace Planar
{
    public class Force : IForce
    {
        public Force(Position vectposition)
        {
            VectorPosition = vectposition;
        }
        public Position VectorPosition { get; set; }
        public static Force operator +(Force first, Force second)
        {
            Position pos = first.VectorPosition + second.VectorPosition;
            return new Force(pos);
        }

        public static Force operator -(Force first, Force second)
        {
            Position pos = first.VectorPosition - second.VectorPosition;
            return new Force(pos);
        }

        public double GetLength()
        {
            return VectorPosition.GetDistance(Position.GetNullVector(VectorPosition.Dimension));    //vector length is the distance of the end point position from the null vector position
        }

        public void Resize(double size)
        {
            double currSize = GetLength();
            for (int i = 0; i < VectorPosition.Dimension; i++)
            {
                if (currSize == 0)
                {
                    throw new Exception("Jeje");
                }
                VectorPosition[i] /= currSize;
                VectorPosition[i] *= size;
            }
        }
    }
}