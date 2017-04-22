namespace Structure
{
    public class HeightmapLayer : Layer
    {

        public HeightmapLayer(Cell[,] cells)
            : base(cells)
        {
        }

        public HeightmapLayer(float[,] heights) 
            : this(Convert(heights))
        {
        }

        private static Cell[,] Convert(float[,] heights)
        {
            var result = new Cell[0, 0];

            for (int x = 0; x < heights.GetLength(0); x++)
            {
                for (int y = 0; y < heights.GetLength(1); y++)
                {
                    var c = new Cell();
                    c.Data = heights[x, y];
                    result[x, y] = c;
                }
            }

            return result;
        }

    }
}
