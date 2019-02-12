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
        public void PlatoTest()
        {
            var context = new WaterContext(_map);
            var drop = new WaterMass(0.1);

            context.AddDrop(drop, (2, 2));
            context.Step();
            var drops = context.Drops;

            Assert.Equal(4, drops.Count);
        }

    }

}