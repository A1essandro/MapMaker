using System;

namespace Generators
{
    public class Noise : GeneratorAlgorithm<NoiseConfig>
    {

        public Noise(NoiseConfig config) : base(config)
        {
            Terra = new float[Config.Size, Config.Size];
        }

        public override float[,] Generate()
        {
            var octaves = getOctaves();
            for (int k = 0; k < octaves; k++)
            {
                octave(k);
            }

            return Terra;
        }

        private void octave(int octave)
        {
            var freq = (int)Math.Pow(2, octave);
            var amp = Math.Pow(Config.Persistence, octave);

            float[,] arr = new float[freq + 1, freq + 1];
            for (var j = 0; j < freq + 1; j++)
            {
                for (var i = 0; i < freq + 1; i++)
                {
                    arr[j, i] = (float)(Config.Random.NextDouble() * amp);
                }
            }

            var nx = Config.Size / freq + 1;
            var ny = Config.Size / freq + 1;

            for (var ky = 0; ky < Config.Size; ky++)
            {
                for (var kx = 0; kx < Config.Size; kx++)
                {
                    var i = kx / nx;
                    var j = ky / ny;
                    var i1 = i + 1;
                    var j1 = j + 1;

                    var dx0 = kx - i * nx;
                    var dx1 = nx - dx0;
                    var dy0 = ky - j * ny;
                    var dy1 = ny - dy0;
                    var z = (arr[j, i] * dx1 * dy1
                            + arr[j, i1] * dx0 * dy1
                            + arr[j1, i] * dx1 * dy0
                            + arr[j1, i1] * dx0 * dy0)
                        / (nx * ny);

                    Terra[kx, ky] += z;
                }
            }
        }

        private int getOctaves()
        {
            return (int)Math.Log(Config.Size, 2);
        }

    }
}
