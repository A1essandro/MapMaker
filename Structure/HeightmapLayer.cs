namespace Structure
{
    public class HeightmapLayer : Layer<float>
    {

        /// <summary>
        /// Minimal height in layer
        /// </summary>
        public float MinHeight { get; protected set; }

        /// <summary>
        /// Maximal height in layer
        /// </summary>
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
