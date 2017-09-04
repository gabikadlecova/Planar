using System;
using System.CodeDom;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using Planar.Interfaces;

namespace Planar
{
    public class Position
    {
        private const double Precision = 0.0005;

        public static Position GetNullVector(int dimension)
        {
            Position pos = new Position(dimension);
            for (int i = 0; i < dimension; i++)
            {
                pos[i] = 0;
            }
            return pos;
        }

        public Position(int dimension)
        {
            Coordinates = new double[dimension];
        }

        public Position(params double[] coors)
        {
            Coordinates = coors;
        }
    

        public void MoveByVector(IForce force)
        {
            if (force.VectorPosition.Dimension != Dimension)
            {
                throw new ArgumentException("Invalid dimension.");
            }
            for (int i = 0; i < Dimension; i++)
            {
                Coordinates[i] += force.VectorPosition[i];
            }
        }

        public int Dimension
        {
            get { return Coordinates.Length; }
        }
        private double[] Coordinates
        {
            get;
        }

        public double this[int i]
        {
            get
            {
                return Coordinates[i];
            }
            set
            {
                Coordinates[i] = value;
            }
                
        }

        public Position ConvertDimensions(Position p1, Position p2)
        {
            if (p1.Dimension >= p2.Dimension)
                return p1;
            Position newPos = new Position(p2.Dimension);
            for (int i = 0; i < p2.Dimension; i++)
            {
                if (i <= p1.Dimension)
                {
                    newPos[i] = p1[i];
                }
                else
                {
                    newPos[i] = 0;
                }
            }
            return newPos;
        }
        public double GetDistance(Position other)
        {
            if (other.Dimension != Dimension)
                throw new ArgumentException("Dimensions must be the same.");
            double result = 0;
            double val;
            for (int i = 0; i < Dimension; i++)
            {
                val = this[i] - other[i];
                result += val * val;
            }
            return Math.Sqrt(result);
        }

        public static Position operator+(Position p1, Position p2)
        {
            if (p1.Dimension != p2.Dimension)
                throw new ArgumentException("Dimensions must be the same.");
            Position pos = new Position(p1.Dimension);
            for (int i = 0; i < p1.Dimension; i++)
            {
                pos[i] = p1[i] + p2[i];
            }
            return pos;
        }

        public static Position operator-(Position p1, Position p2)
        {
            if (p1.Dimension != p2.Dimension)
                throw new ArgumentException("Dimensions must be the same.");
            Position pos = new Position(p1.Dimension);
            for (int i = 0; i < p1.Dimension; i++)
            {
                pos[i] = p1[i] - p2[i];
            }
            return pos;
        }

        public override bool Equals(object obj) //if needed, less precise (epsilon < 0.0005) "IsCloseTo" method could be implementated
        {
            if (!(obj is Position)) return false;
            Position pos = (Position) obj;

            if (Dimension != pos.Dimension) return false;
            for (int i = 0; i < Dimension; i++)
            {
                double sub = this[i] - pos[i];
                if (sub > Precision || sub < - Precision)
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int res = 131;
            for (int i = 0; i < Dimension; i++)
            {
                res = (int)Math.Ceiling(this[i]) * 71 - i * 131;
            }
            return res;
        }

        public static bool operator ==(Position p1, Position p2)
        {
            if (object.ReferenceEquals(p1, null))
            {
                if (object.ReferenceEquals(p2, null)) return true;
                return false;
            }
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !(p1 == p2);
        }
    }
}