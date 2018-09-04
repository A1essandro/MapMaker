using Generators;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Generators
{

    public class DiamondSquareConfigTest
    {

        [Fact]
        public void TestSize()
        {
            var config = new DiamondSquareConfig(3);
            Assert.Equal(9, config.Size);
        }

        [Fact]
        public void TestRandomSeed()
        {
            var configEq1 = new DiamondSquareConfig(3, 1, new System.Random(5));
            var configEq2 = new DiamondSquareConfig(3, 1, new System.Random(5));
            var configNotEq = new DiamondSquareConfig(3, 1, new System.Random(10));

            var generatorEq1 = new DiamondSquare(configEq1);
            var generatorEq2 = new DiamondSquare(configEq2);
            var generatorNotEq = new DiamondSquare(configNotEq);

            var mapEq1 = generatorEq1.GenerateAsync();
            var mapEq2 = generatorEq2.Generate();
            var mapnotEq = generatorNotEq.GenerateAsync();

            Assert.Equal(mapEq2[1, 2],
                mapEq1.GetAwaiter().GetResult()[1, 2]);

            Assert.NotEqual(mapnotEq.GetAwaiter().GetResult()[1, 2],
                mapEq1.GetAwaiter().GetResult()[1, 2]);
        }
    }
}
