using System.Linq;
using System.Threading.Tasks;
using Generators;
using Structure.Impl;
using Xunit;

namespace Tests.Structure
{

    public class HeightmapLayerTest
    {

        private Task<float[,]> _heightsTask;

        public HeightmapLayerTest()
        {
            var config = new DiamondSquareConfig(3);
            var generator = new DiamondSquare(config);
            _heightsTask = generator.GenerateAsync();
        }

        [Fact]
        public void TestGetCell()
        {
            var heights = _heightsTask.GetAwaiter().GetResult();
            var layer = new HeightmapLayer(heights);

            Assert.Equal(heights[0, 1], layer.GetCell(0, 1));
        }

        [Fact]
        public void TestMinHeight()
        {
            var heights = _heightsTask.GetAwaiter();
            var layer = new HeightmapLayer(heights.GetResult());

            var minHeight = layer.Select(h => h.Data).Concat(new[] { float.MaxValue }).Min();

            Assert.Equal(minHeight, layer.MinHeight);
        }

        [Fact]
        public void TestMaxHeight()
        {
            var heights = _heightsTask.GetAwaiter().GetResult();
            var layer = new HeightmapLayer(heights);

            var maxHeight = layer.Select(h => h.Data).Concat(new[] { float.MinValue }).Max();

            Assert.Equal(maxHeight, layer.MaxHeight);
        }

    }
}
