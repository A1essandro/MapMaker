using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Generators;
using Structure.Impl;

namespace Tests.Structure
{
    [TestClass]
    public class HeightmapLayerTest
    {

        private Task<float[,]> _heightsTask;

        [TestInitialize]
        public void Initialize()
        {
            var config = new DiamondSquareConfig(3);
            var generator = new DiamondSquare(config);
            _heightsTask = generator.GenerateAsync();
        }

        [TestMethod]
        public void TestGetCell()
        {
            var heights = _heightsTask.GetAwaiter().GetResult();
            var layer = new HeightmapLayer(heights);

            Assert.AreEqual(heights[0, 1], layer.GetCell(0, 1));
        }

        [TestMethod]
        public void TestMinHeight()
        {
            var heights = _heightsTask.GetAwaiter();
            var layer = new HeightmapLayer(heights.GetResult());

            var minHeight = layer.Select(h => h.Data).Concat(new[] {float.MaxValue}).Min();

            Assert.AreEqual(minHeight, layer.MinHeight);
        }

        [TestMethod]
        public void TestMaxHeight()
        {
            var heights = _heightsTask.GetAwaiter().GetResult();
            var layer = new HeightmapLayer(heights);

            var maxHeight = layer.Select(h => h.Data).Concat(new[] {float.MinValue}).Max();

            Assert.AreEqual(maxHeight, layer.MaxHeight);
        }
    }
}
