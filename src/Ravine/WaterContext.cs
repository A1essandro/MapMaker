using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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

        private readonly double[,] _heigthmap;
        private readonly IDictionary<WaterMass, Vector> _drops = new Dictionary<WaterMass, Vector>();

        public void AddDrop(WaterMass drop, (int, int) p) => AddDrop(drop, new Vector(p.Item1, p.Item2));

        public IDictionary<WaterMass, Vector> Drops => _drops;

        public WaterContext(double[,] heigthmap) => _heigthmap = heigthmap;

        public void AddDrop(WaterMass drop, Vector position) => _drops.Add(drop, position);

        public void Step(Func<Vector, IEnumerable<Vector>> neighborsGetter = null)
        {
            if (neighborsGetter == null)
                neighborsGetter = DefaultNeighborsGetter;

            var newDrops = new ConcurrentDictionary<WaterMass, Vector>();

            Parallel.ForEach(_drops, drop =>
            {
                var currentDropPosition = drop.Value;
                var dropObj = drop.Key;
                var moveRanks = _getMoveRanks(neighborsGetter, currentDropPosition, dropObj);
                var rankSum = moveRanks.Sum(x => x.Value);
                var moveFactors = _getMoveFactors(moveRanks, rankSum);
                foreach (var targetCell in moveFactors)
                {
                    var watermassFactor = targetCell.Value;
                    newDrops.TryAdd(new WaterMass(dropObj.Mass * watermassFactor), targetCell.Key);
                }
            });

            _drops.Clear();
            foreach (var drop in newDrops)
            {
                _drops.Add(drop.Key, drop.Value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Dictionary<Vector, double> _getMoveFactors(Dictionary<Vector, double> moveRanks, double rankSum)
        {
            Dictionary<Vector, double> moveFactors;
            if (rankSum > 0)
                moveFactors = moveRanks.ToDictionary(x => x.Key, x => x.Value / rankSum);
            else
                moveFactors = moveRanks.ToDictionary(x => x.Key, x => 1.0 / moveRanks.Count);
            return moveFactors;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Dictionary<Vector, double> _getMoveRanks(Func<Vector, IEnumerable<Vector>> neighborsGetter, Vector currentDropPosition, WaterMass dropObj)
        {
            var moveRanks = new Dictionary<Vector, double>();
            foreach (var targetCell in neighborsGetter(currentDropPosition))
            {
                if (!_isInMap(targetCell))
                    continue;
                var rank = _getHeight(currentDropPosition) - _getHeight(targetCell);
                if (rank < 0)
                    continue;
                var range = Math.Sqrt(Math.Pow(currentDropPosition.X + dropObj.Speed.X - targetCell.X, 2)
                    + Math.Pow(currentDropPosition.Y + dropObj.Speed.Y - targetCell.Y, 2));
                var factor = range != 0 ? range : 0.00001;
                moveRanks[targetCell] = rank / factor;
            }

            return moveRanks;
        }

        private double _getHeight(Vector pos) => _heigthmap[pos.X, pos.Y];

        private bool _isInMap(Vector pos) =>
            pos.X >= 0 && pos.Y >= 0 && pos.X < _heigthmap.GetLength(0) && pos.Y < _heigthmap.GetLength(1);

    }
}