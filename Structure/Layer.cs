using System.Collections;
using System.Collections.Generic;

namespace Structure
{

    /// <summary>
    /// Layer for map. Iterable, each iteration return cell (x->y)
    /// </summary>
    /// <typeparam name="TCell"></typeparam>
    public abstract class Layer<TCell> : IEnumerable<TCell>
    {

        private readonly TCell[,] _cells;

        protected Layer(TCell[,] cells)
        {
            _cells = cells;
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
        public IEnumerator<TCell> GetEnumerator()
        {
            for (var x = 0; x < _cells.GetLength(0); x++)
            {
                for (var y = 0; y < _cells.GetLength(0); y++)
                {
                    yield return GetCell(x, y);
                }
            }
        }

        /// <summary>
        /// IEnumerable implementation
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}