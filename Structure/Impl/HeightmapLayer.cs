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
                if (cell.Data > MaxHeight)
                {
                    MaxHeight = cell.Data;
                }
                else if (cell.Data < MinHeight)
                {
                    MinHeight = cell.Data;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cells"></param>
        public static HeightmapLayer Mix(HeightmapLayer layer1, HeightmapLayer layer2)
        {
            var sizeX = Math.Min(layer1.SizeX, layer2.SizeX);
            var sizeY = Math.Min(layer1.SizeY, layer2.SizeY);

            var cells = new float[sizeX, sizeY];

            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    cells[x, y] = layer1.GetCell(x, y) + layer2.GetCell(x, y);
                }
            }

            return new HeightmapLayer(cells);
        }

    }
}
