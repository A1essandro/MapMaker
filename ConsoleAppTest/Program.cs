using Generators;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new DiamondSquareConfig(7);
            var generator = new DiamondSquare(config);
            var a = generator.GenerateAsync();
            var b = a.Result;
        }

    }
}
