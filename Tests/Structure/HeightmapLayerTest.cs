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

        [TestMethod]
        public void TestMinHeight()
        {
            var heights = heightsTask.GetAwaiter().GetResult();
            var layer = new HeightmapLayer(heights);

            float minHeight = float.MaxValue;
            foreach(var h in layer)
            {
                if(h < minHeight)
                {
                    minHeight = h;
                }
            }

            Assert.AreEqual(minHeight, layer.MinHeight);
        }

        [TestMethod]
        public void TestMaxHeight()
        {
            var heights = heightsTask.GetAwaiter().GetResult();
            var layer = new HeightmapLayer(heights);

            float maxHeight = float.MinValue;
            foreach (var h in layer)
            {
                if (h > maxHeight)
                {
                    maxHeight = h;
                }
            }

            Assert.AreEqual(maxHeight, layer.MaxHeight);
        }
    }
}
