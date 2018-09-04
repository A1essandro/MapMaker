using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VectorClass;

namespace Ravine
{
    public class Map<T>
    {

        private T[,] _initialMap;
        private Analizer _analizer;

        public Map(T[,] heightmap)
        {
            _initialMap = heightmap;
            _analizer = new Analizer(this);
        }

        public IDictionary<Vector2D_Int, double> GetNear(Vector2D_Int position)
        {
            var result = new Dictionary<Vector2D_Int, double>();
            var unprepared = new Dictionary<Vector2D_Int, dynamic>();
            dynamic sum = 0;

            for (var diffX = -1; diffX <= 1; diffX++)
            {
                for (var diffY = -1; diffY <= 1; diffY++)
                {
                    if ((diffX == 0 && diffY == 0) 
                        || diffX + position.X < 0
                        || diffY + position.Y < 0
                        || diffX + position.X >= _analizer.SizeX
                        || diffY + position.Y >= _analizer.SizeY)
                        continue;

                    dynamic current = _initialMap[position.X + diffX, position.Y + diffY];
                    dynamic near = _initialMap[position.X, position.Y];
                    var diff = current - near;
                    if (diff > 0)
                    {
                        result.Add(new Vector2D_Int(position.X, position.Y), diff);
                        sum += diff;
                    }

                }
            }

            foreach (var element in unprepared)
            {
                result.Add(element.Key, (double)(element.Value / sum));
            }

            return result;
        }

        #region Analizer

        private class Analizer
        {

            private Map<T> _map;

            public int SizeX { get; private set; }
            public int SizeY { get; private set; }

            public T MaxHeight { get; private set; }
            public T MinHeight { get; private set; }

            public Analizer(Map<T> map)
            {
                _map = map;
                _analize();
            }

            private void _analize()
            {
                SizeX = _map._initialMap.GetLength(0);
                SizeY = _map._initialMap.GetLength(1);

                var comparer = Comparer<T>.Default;

                for (int x = 0; x < SizeX; x++)
                {
                    for (int y = 0; y < SizeY; y++)
                    {
                        if (comparer.Compare(_map._initialMap[x, y], MaxHeight) > 0)
                        {
                            MaxHeight = _map._initialMap[x, y];
                        }
                        else if (comparer.Compare(_map._initialMap[x, y], MinHeight) < 0)
                        {
                            MinHeight = _map._initialMap[x, y];
                        }
                    }
                }
            }

        }

        #endregion

    }
}
