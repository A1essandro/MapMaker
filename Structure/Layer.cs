using System.Collections;
using System.Collections.Generic;

namespace Structure
{
    public abstract class Layer<TCell> : IEnumerable<TCell>
    {

        private TCell[,] Cells { get; set; } 

        protected Layer(TCell[,] cells)
        {
            Cells = cells;
        }

        public TCell GetCell(int x, int y)
        {
            return Cells[x, y];
        }

        public IEnumerator<TCell> GetEnumerator()
        {
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(0); y++)
                {
                    yield return GetCell(x, y);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}