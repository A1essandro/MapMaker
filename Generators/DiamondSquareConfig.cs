using System;

namespace Generators
{
    public class DiamondSquareConfig
    {

        public int Size { get; private set; }

        public float Persistence { get; private set; }

        public DiamondSquareConfig(byte sizePower, float persistence = 1)
        {
            Size = PowerToSize(sizePower);
            Persistence = persistence;
        }

        static public int PowerToSize(int sizePower)
        {
            return (int)Math.Pow(2, sizePower) + 1;
        }

    }
}
