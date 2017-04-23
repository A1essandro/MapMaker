using Generators;
using Structure;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(10);
            var generator = new DiamondSquare(config);
            var a = generator.GenerateAsync();
            var t = a.GetAwaiter().GetResult();

            var m = new Map();
            m.AddLayer<HeightmapLayer, float>(new HeightmapLayer(t));
            var layer = m.GetLayer<HeightmapLayer, float>();
            var c = layer.GetCell(0, 0);
        }

    }
}
