using Generators;
using Structure.Impl;
using Xunit;

namespace Tests.Structure
{

    public class OverlaySumTest
    {
        [Fact]
        public void TestOverlay()
        {
            var pnConfig1 = new NoiseConfig(100, 0.7);
            var generator1 = new Noise(pnConfig1);
            var h1 = generator1.GenerateAsync();

            var pnConfig2 = new NoiseConfig(100, 0.25);
            var generator2 = new Noise(pnConfig2);
            var h2 = generator2.GenerateAsync();

            var hlayer1 = new HeightmapLayer(h1.Result);
            var hlayer2 = new HeightmapLayer(h2.Result);

            var hlayerCells = HeightmapLayer.Overlay(hlayer1, hlayer2, new OverlayFloatSum());
            var hlayer = new HeightmapLayer(hlayerCells);

            Assert.Equal(h1.Result[50, 50] + hlayer2.GetCell(50, 50), hlayer.GetCell(50, 50));
        }
    }
}
