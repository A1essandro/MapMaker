namespace Structure
{
    public abstract class Layer
    {

        protected Cell[,] Cells { get; private set; } 

        public Layer(Cell[,] cells)
        {
            Cells = cells;
        }

        public TReturn GetCell<TReturn>(int x, int y)
        {
            return (TReturn)Cells[x, y].Data;
        }

    }
}