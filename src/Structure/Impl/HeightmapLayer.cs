using System;

namespace Structure.Impl
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

        /// <summary>
        /// Value between max and min
        /// </summary>
        public float OverallHeight { get; set; }

        public HeightmapLayer(float[,] cells)
            : base(cells)
        {
            Analize();
        }

        public static float[,] NormalizeHeights(HeightmapLayer layer)
        {
            var result = new float[layer.SizeX, layer.SizeY];
            for (var x = 0; x < layer.SizeX; x++)
            {
                for (var y = 0; y < layer.SizeY; y++)
                {
                    result[x, y] = (layer.GetCell(x, y) - layer.MinHeight) / layer.OverallHeight;
                }
            }
            return result;
        }

        private void Analize()
        {
            MaxHeight = float.MinValue;
            MinHeight = float.MaxValue;
            foreach (var cell in this)
            {
                if (cell.Data > MaxHeight)
                {
                    MaxHeight = cell.Data;
                }
                else if (cell.Data < MinHeight)
                {
                    MinHeight = cell.Data;
                }
            }
            OverallHeight = MaxHeight - MinHeight;
        }

    }
}
