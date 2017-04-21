using Generators;
using System.Diagnostics;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(3);
            var generator = new DiamondSquare(config);

            var a = generator.Generate();
        }
    }
}
