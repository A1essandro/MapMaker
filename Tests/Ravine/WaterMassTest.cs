using System.Linq;
using System.Threading.Tasks;
using Generators;
using MapMaker.Ravine;
using Structure.Impl;
using Xunit;

namespace Tests.Ravine
{

    class WaterMassTest
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
            var initial = new WaterMass(0.1, new Vector(2, 2));

        }

    }

}