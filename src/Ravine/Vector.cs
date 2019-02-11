using System;

namespace MapMaker.Ravine
{
    public struct Vector
    {

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);

        public double GetLength() => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

    }
}