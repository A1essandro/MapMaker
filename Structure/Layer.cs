namespace Structure
{
    public abstract class Layer<TCell>
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

    }
}