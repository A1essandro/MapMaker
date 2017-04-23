namespace Structure
{
    public abstract class Layer<TCell>
    {

        private TCell[,] Cells { get; set; } 

        public Layer(TCell[,] cells)
        {
            Cells = cells;
        }

        public TCell GetCell<TReturn>(int x, int y)
        {
            return Cells[x, y];
        }

    }
}