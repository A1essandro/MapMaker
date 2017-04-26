using System.Linq;

namespace Structure.Impl
{
    public class WaterLayer : Layer<bool>
    {

        public float Waterline { get; }

        public WaterLayer(HeightmapLayer layer, float waterline)
            : base(Convert(layer, waterline))
        {
            Waterline = waterline;
        }

        private static bool[,] Convert(HeightmapLayer layer, float waterline)
        {
            var result = new bool[layer.SizeX, layer.SizeY];
            foreach (var cell in layer)
            {
                result[cell.X, cell.Y] = cell.Data < waterline;
            }

            return result;
        }

        public static float GetWaterlineFromPercentage(HeightmapLayer layer, float hPercent)
        {
            return (layer.MaxHeight - layer.MinHeight) * hPercent + layer.MinHeight;
        }

    }
}
