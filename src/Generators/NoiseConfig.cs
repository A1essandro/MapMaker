using System;

namespace Generators
{
    public class NoiseConfig
    {

        public int Size { get; private set; }

        public double Persistence { get; private set; }

        /// <summary>
        /// Use like hash
        /// </summary>
        public Random Random { get; private set; }

        public NoiseConfig(int size, double persistence = 0.5, Random random = null)
        {
            Size = size;
            Persistence = persistence;
            Random = random ?? new Random();
        }

    }
}
