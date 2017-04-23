using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Generators;
using Structure;

namespace Tests.Structure
{
    [TestClass]
    public class HeightmapLayerTest
    {

        Task<float[,]> heightsTask;

        [TestInitialize]
        public void Initialize()
        {
            var config = new DiamondSquareConfig(3);
            var generator = new DiamondSquare(config);
            heightsTask = generator.GenerateAsync();
        }

        [TestMethod]
        public void TestGetCell()
        {
            var heights = heightsTask.GetAwaiter().GetResult();
            var layer = new HeightmapLayer(heights);

            Assert.AreEqual(heights[0, 1], layer.GetCell(0, 1));           
        }
    }
}
