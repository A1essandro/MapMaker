namespace Structure
{
    public class HeightmapLayer : Layer<float>
    {

        public float MinHeight { get; protected set; }
        public float MaxHeight { get; protected set; }

        public HeightmapLayer(float[,] cells)
            : base(cells)
        {
            Analize();
        }

        private void Analize()
        {
            MaxHeight = float.MinValue;
            MinHeight = float.MaxValue;
            foreach(var cell in this)
            {
                if (cell > MaxHeight)
                {
                    MaxHeight = cell;
                }
                else if (cell < MinHeight)
                {
                    MinHeight = cell;
                }
            }
        }

    }
}
