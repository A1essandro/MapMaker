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

        public readonly static Func<Vector, IEnumerable<Vector>> DefaultNeighborsGetter = (Vector pos) =>
        {
            return new List<Vector>
            {
                new Vector(pos.X, pos.Y - 1),
                new Vector(pos.X, pos.Y + 1),
                new Vector(pos.X - 1, pos.Y),
                new Vector(pos.X + 1, pos.Y)
            };
        };

        public readonly static Func<double, double> DefaultAbsorbtion = oldVal => oldVal - 0.015; //TODO: Magic constant...

        private readonly double[,] _heightmap;
        private readonly IDictionary<WaterDrop, Vector> _drops = new Dictionary<WaterDrop, Vector>();

        public IDictionary<WaterDrop, Vector> Drops => _drops;

        public void AddDrop(WaterDrop drop, (int, int) p) => AddDrop(drop, new Vector(p.Item1, p.Item2));

        public WaterContext(double[,] heightmap) => _heightmap = heightmap;

        public void AddDrop(WaterDrop drop, Vector position) => _drops.Add(drop, position);

        public void Step(Func<Vector, IEnumerable<Vector>> neighborsGetter = null, Func<double, double> absobtion = null)
        {
            PropagateWater(neighborsGetter ?? DefaultNeighborsGetter);
            Merge();
            Absorb(absobtion ?? DefaultAbsorbtion);
        }

        /// <summary>
        /// Propagation drops on the map
        /// </summary>
        /// <param name="neighborsGetter"></param>
        public void PropagateWater(Func<Vector, IEnumerable<Vector>> neighborsGetter)
        {
            var newDrops = new ConcurrentDictionary<WaterDrop, Vector>();

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
                    newDrops.TryAdd(new WaterDrop(dropObj.Mass * watermassFactor), targetCell.Key);
                }
            });
            _peplaceDrops(newDrops);
        }

        /// <summary>
        /// Merge all drops are in the same cells
        /// </summary>
        public void Merge()
        {
            var groups = _drops.GroupBy(x => x.Value).Where(x => x.Count() > 1).ToArray();
            foreach (var group in groups)
            {
                var unmerged = group.Select(x => x.Key).ToArray();
                var merged = unmerged.Aggregate((total, next) => total + next);
                foreach (var key in unmerged)
                    _drops.Remove(key);
                _drops.Add(merged, group.Key);
            }
        }

        /// <summary>
        /// Step of water absorption into the soil 
        /// </summary>
        /// <param name="absobtion"></param>
        public void Absorb(Func<double, double> absobtion)
        {
            var drops = _drops.ToArray();
            _drops.Clear();
            foreach (var drop in drops)
            {
                var newMass = DefaultAbsorbtion(drop.Key.Mass);
                _drops.Add(new WaterDrop(newMass), drop.Value);
            }
        }

        private void _peplaceDrops(IDictionary<WaterDrop, Vector> newDrops)
        {
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
        private Dictionary<Vector, double> _getMoveRanks(Func<Vector, IEnumerable<Vector>> neighborsGetter, Vector currentDropPosition, WaterDrop dropObj)
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

        private double _getHeight(Vector pos) => _heightmap[pos.X, pos.Y];

        private bool _isInMap(Vector pos) =>
            pos.X >= 0 && pos.Y >= 0 && pos.X < _heightmap.GetLength(0) && pos.Y < _heightmap.GetLength(1);

    }
}