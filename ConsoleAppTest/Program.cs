using System.Diagnostics;
using Generators;
using Structure;
using System.Drawing;
using System;

namespace ConsoleAppTest
{
    class Program
    {

        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(10, 5);
            var generator = new DiamondSquare(config);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var b = generator.Generate();
            var m = new Map();
            m.AddLayer<HeightmapLayer, float>(new HeightmapLayer(b));

            CreateImage(config, m.GetLayer<HeightmapLayer, float>());

            stopWatch.Stop();
            Debug.WriteLine(stopWatch.Elapsed);
        }

        private static void CreateImage(DiamondSquareConfig config, HeightmapLayer layer)
        {
            Bitmap bmp = new Bitmap(config.Size, config.Size);
            for (var x = 0; x < config.Size; x++)
            {
                for (var y = 0; y < config.Size; y++)
                {
                    var color = 255 * Math.Abs((layer.GetCell(x, y) - layer.MinHeight)
                        / (layer.MaxHeight - layer.MinHeight));
                    bmp.SetPixel(x, y, Color.FromArgb((int)color, (int)color, (int)color));
                }
            }

            bmp.Save("test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

    }
}
