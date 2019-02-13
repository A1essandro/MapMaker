using System.Linq;
using System.Threading.Tasks;
using Generators;
using MapMaker.Ravine;
using Structure.Impl;
using Xunit;

namespace Tests.Ravine
{

    public class WaterMassTest
    {
        private double[,] _map = new double[,] {
                { 0.5, 0.5, 0.5, 0.5, 0.5 },
                { 0.5, 0.5, 0.5, 0.5, 0.5 },
                { 0.5, 0.5, 0.5, 0.5, 0.5 },
                { 0.5, 0.5, 0.5, 0.5, 0.5 },
                { 0.5, 0.5, 0.5, 0.5, 0.5 },
            };

        [Fact]
        public void PropagateTest()
        {
            var context = new WaterContext(_map);
            var drop = new WaterDrop(0.1);

            context.AddDrop(drop, (2, 2));
            context.PropagateWater(WaterContext.DefaultNeighborsGetter);
            var drops = context.Drops;

            Assert.Equal(4, drops.Count);
        }

        [Fact]
        public void MergeTest()
        {
            var context = new WaterContext(_map);
            var drop = new WaterDrop(0.1);

            context.AddDrop(drop, (2, 2));
            context.PropagateWater(WaterContext.DefaultNeighborsGetter);
            context.PropagateWater(WaterContext.DefaultNeighborsGetter);
            var dropsBefore = context.Drops.ToDictionary(x => x.Key, x => x.Value);
            context.Merge();
            var dropsAfter = context.Drops.ToDictionary(x => x.Key, x => x.Value);

            Assert.Equal(16, dropsBefore.Count);
            Assert.Equal(9, dropsAfter.Count);
        }

    }

}