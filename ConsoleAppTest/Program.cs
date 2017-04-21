using Generators;
using System.Diagnostics;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(2);
            var generator = new DiamondSquare(config);

            var a = generator.Generate();
            Debug.WriteLine(a);
        }
    }
}
