using System.Collections;
using System.Collections.Generic;

namespace Structure
{

    /// <summary>
    /// Layer for map. Iterable, each iteration return cell (x->y)
    /// </summary>
    /// <typeparam name="TCell"></typeparam>
    public abstract class Layer<TCell> : IEnumerable<Layer<TCell>.CellInfo>
    {

        private readonly TCell[,] _cells;

        public readonly int SizeX;
        public readonly int SizeY;

        protected Layer(TCell[,] cells)
        {
            _cells = cells;
            SizeX = cells.GetLength(0);
            SizeY = cells.GetLength(1);
        }

        /// <summary>
        /// Get cell by coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public TCell GetCell(int x, int y)
        {
            return _cells[x, y];
        }

        /// <summary>
        /// IEnumerable implementation
        /// </summary>
        /// <returns></returns>
        public IEnumerator<CellInfo> GetEnumerator()
        {
            for (var x = 0; x < _cells.GetLength(0); x++)
            {
                for (var y = 0; y < _cells.GetLength(0); y++)
                {
                    yield return new CellInfo(x, y, GetCell(x, y));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class CellInfo
        {

            protected internal CellInfo(int x, int y, TCell data)
            {
                X = x;
                Y = y;
                Data = data;
            }

            public int X { get; private set; }

            public int Y { get; private set; }

            public TCell Data { get; private set; }

        }

    }
}