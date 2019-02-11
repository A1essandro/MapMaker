using System;
using System.Collections.Generic;
using System.Linq;

namespace MapMaker.Ravine
{
    public class WaterContext
    {

        private readonly static Func<Vector, IEnumerable<Vector>> DefaultNeighborsGetter = (Vector pos) =>
        {
            return new List<Vector>
            {
                new Vector(pos.X, pos.Y - 1),
                new Vector(pos.X, pos.Y + 1),
                new Vector(pos.X - 1, pos.Y),
                new Vector(pos.X + 1, pos.Y)
            };
        };

        private readonly int[,] _heigthmap;
        private readonly IDictionary<WaterMass, Vector> _drops = new Dictionary<WaterMass, Vector>();

        public WaterContext(int[,] heigthmap) => _heigthmap = heigthmap;

        public void AddDrop(WaterMass drop, Vector position) => _drops.Add(drop, position);

        public void Step(Func<Vector, IEnumerable<Vector>> neighborsGetter = null)
        {
            if (neighborsGetter == null)
                neighborsGetter = DefaultNeighborsGetter;

            var newDrops = new Dictionary<WaterMass, Vector>();

            foreach (var drop in _drops)
            {
                var currentDropPosition = drop.Value;
                var dropObj = drop.Key;
                var moveRanks = new Dictionary<Vector, double>();
                foreach (var targetCell in neighborsGetter(currentDropPosition))
                {
                    var rank = _getHeight(currentDropPosition) - _getHeight(targetCell);
                    if (rank < 0)
                        continue;
                    var speedToPositionVector = new Vector(dropObj.Speed.X - targetCell.X, dropObj.Speed.Y - targetCell.Y);
                    var factor = speedToPositionVector.GetLength() != 0 ? speedToPositionVector.GetLength() : 0.001;
                    moveRanks[targetCell] = rank / factor;
                }
                var rankSum = moveRanks.Sum(x => x.Value);
                var moveFactors = moveRanks.ToDictionary(x => x.Key, x => x.Value / rankSum);
                foreach (var targetCell in moveFactors)
                {
                    var watermassFactor = targetCell.Value;
                    newDrops.Add(new WaterMass(dropObj.Mass * watermassFactor, targetCell.Key), targetCell.Key);
                }

                _drops.Remove(drop.Key);
            }
        }

        private double _getHeight(Vector pos) => _heigthmap[pos.X, pos.Y];

    }
}