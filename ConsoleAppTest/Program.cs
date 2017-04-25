using System.Diagnostics;
using Generators;
using Structure;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(3);
            var generator = new DiamondSquare(config);
            
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var a = generator.GenerateAsync();
            var b = a.Result;
            var m = new Map();
            m.AddLayer<HeightmapLayer, float>(new HeightmapLayer(b));
            stopWatch.Stop();
            Debug.WriteLine(stopWatch.Elapsed);
        }

    }
}
