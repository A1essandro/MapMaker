using System.Diagnostics;
using Generators;
using Structure;
using System.Drawing;
using System.Drawing.Imaging;
using Structure.Impl;

namespace ConsoleAppTest
{
    class Program
    {

        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(10, 2f);
            var generator = new DiamondSquare(config);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var b = generator.Generate();
            var m = new Map();
            var hLayer = new HeightmapLayer(b);
            m.AddLayer<HeightmapLayer, float>(hLayer);
            var waterLine = WaterLayer.GetWaterlineFromPercentage(hLayer, 0.35f);
            var waterLayer = new WaterLayer(hLayer, waterLine);
            m.AddLayer<WaterLayer, bool>(waterLayer);

            CreateImage(config, hLayer, waterLayer);

            stopWatch.Stop();
            Debug.WriteLine(stopWatch.Elapsed);
        }

        private static void CreateImage(DiamondSquareConfig config, HeightmapLayer layer, WaterLayer waterLayer)
        {
            var bmp = new Bitmap(config.Size, config.Size);
            var maxDepth = waterLayer.Waterline - layer.MinHeight;
            var maxHeight = layer.MaxHeight - waterLayer.Waterline;
            for (var x = 0; x < config.Size; x++)
            {
                for (var y = 0; y < config.Size; y++)
                {
                    if (waterLayer.GetCell(x, y))
                    {
                        var depth = waterLayer.Waterline - layer.GetCell(x, y);
                        var g = 255 - (byte)(255 * (depth / maxDepth));
                        var b = 255 - (byte)(100 * (depth / maxDepth));
                        bmp.SetPixel(x, y, Color.FromArgb(0, g, b));
                    }
                    else
                    {
                        var height = layer.GetCell(x, y) - waterLayer.Waterline;
                        var b = (byte)(50 * height / maxHeight);
                        var r = (byte)(255 * height / maxHeight);
                        var g = 175 - (byte)(50 * height / maxHeight);
                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }

            var codec = GetEncoder(ImageFormat.Jpeg);
            var filename = "test.jpg";
            bmp.Save(filename, codec, GetEncoderParameters());
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private static EncoderParameters GetEncoderParameters()
        {
            var myEncoder = Encoder.Quality;
            var encoderParams = new EncoderParameters(1);
            var myEncoderParameter = new EncoderParameter(myEncoder, 1000L);
            encoderParams.Param[0] = myEncoderParameter;

            return encoderParams;
        }

    }
}
