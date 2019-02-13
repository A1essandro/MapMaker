using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MapMaker.Ravine
{
    public class Propagator : IPropagator
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
        private readonly Func<Vector, IEnumerable<Vector>> _neighborsGetter;

        public Propagator(Func<Vector, IEnumerable<Vector>> neighborsGetter = null)
        {
            _neighborsGetter = neighborsGetter ?? DefaultNeighborsGetter;
        }

        public IDictionary<WaterDrop, Vector> Propagate(double[,] map, IDictionary<WaterDrop, Vector> drops)
        {
            var newDrops = new ConcurrentDictionary<WaterDrop, Vector>();

            Parallel.ForEach(drops, drop =>
            {
                var currentDropPosition = drop.Value;
                var dropObj = drop.Key;
                var moveRanks = _getMoveRanks(map, currentDropPosition, dropObj);
                var rankSum = moveRanks.Sum(x => x.Value);
                var moveFactors = _getMoveFactors(moveRanks, rankSum);
                foreach (var targetCell in moveFactors)
                {
                    var watermassFactor = targetCell.Value;
                    newDrops.TryAdd(new WaterDrop(dropObj.Mass * watermassFactor), targetCell.Key);
                }
            });

            return newDrops;
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
        private Dictionary<Vector, double> _getMoveRanks(double[,] map, Vector currentDropPosition, WaterDrop dropObj)
        {
            var moveRanks = new Dictionary<Vector, double>();
            foreach (var targetCell in _neighborsGetter(currentDropPosition))
            {
                if (!IsInMap(map, targetCell))
                    continue;
                var rank = GetHeight(map, currentDropPosition) - GetHeight(map, targetCell);
                if (rank < 0)
                    continue;
                var range = Math.Sqrt(Math.Pow(currentDropPosition.X + dropObj.Speed.X - targetCell.X, 2)
                    + Math.Pow(currentDropPosition.Y + dropObj.Speed.Y - targetCell.Y, 2));
                var factor = range != 0 ? range : 0.00001;
                moveRanks[targetCell] = rank / factor;
            }

            return moveRanks;
        }

        private static bool IsInMap(double[,] map, Vector v) => v.X >= 0 && v.Y >= 0 && v.X < map.GetLength(0) && v.Y < map.GetLength(1);

        private double GetHeight(double[,] map, Vector v) => map[v.X, v.Y];

    }
}