using Generators;
using System.Diagnostics;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(10);
            var generator = new DiamondSquare(config);

            Stopwatch stopWatch = new Stopwatch();
            
            Debug.WriteLine("start");
            stopWatch.Start();
            var a = generator.GenerateAsync();
            Debug.WriteLine("check " + stopWatch.Elapsed);

            var t = a.GetAwaiter().GetResult();
            stopWatch.Stop();
            Debug.WriteLine("end " + stopWatch.Elapsed);
        }
    }
}
