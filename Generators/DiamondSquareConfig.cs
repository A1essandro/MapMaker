using System;

namespace Generators
{
    public class DiamondSquareConfig
    {

        public int Size { get; private set; }

        public float Persistence { get; private set; }

        public Random Random { get; private set; }

        public DiamondSquareConfig(byte sizePower, float persistence = 1, Random random = null)
        {
            Size = PowerToSize(sizePower);
            Persistence = persistence;
            Random = random ?? new Random();
        }

        public static int PowerToSize(int sizePower)
        {
            return (int)Math.Pow(2, sizePower) + 1;
        }

    }
}
