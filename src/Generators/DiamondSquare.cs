using System;

namespace Generators
{
    public class DiamondSquare : GeneratorAlgorithm<DiamondSquareConfig>
    {

        #region Public

        public DiamondSquare(DiamondSquareConfig config) : base(config)
        {
        }

        /// <summary>
        /// Async implementation of Generate
        /// </summary>
        /// <returns>Task will returns double array of float</returns>
        public override float[,] Generate()
        {
            Terra = new float[Config.Size, Config.Size];

            var last = Config.Size - 1;
            Terra[0, 0] = _getOffset(Config.Size);
            Terra[0, last] = _getOffset(Config.Size);
            Terra[last, 0] = _getOffset(Config.Size);
            Terra[last, last] = _getOffset(Config.Size);

            _divide(Config.Size);

            return Terra;
        }

        #endregion

        #region Main algorithm methods

        private void _divide(int stepSize)
        {
            int half;
            while ((half = stepSize / 2) >= 1)
            {
                for (var x = half; x < Config.Size; x += stepSize)
                {
                    for (var y = half; y < Config.Size; y += stepSize)
                    {
                        _square(x, y, half, _getOffset(stepSize));
                    }
                }

                stepSize = half;
            }
        }

        private void _square(int x, int y, int size, float offset)
        {
            var a = _getCellHeight(x - size, y - size, size);
            var b = _getCellHeight(x + size, y + size, size);
            var c = _getCellHeight(x - size, y + size, size);
            var d = _getCellHeight(x + size, y - size, size);
            var average = (a + b + c + d) / 4;
            Terra[x, y] = average + offset;
            _diamond(x, y - size, size);
            _diamond(x - size, y, size);
            _diamond(x, y + size, size);
            _diamond(x + size, y, size);
        }

        private void _diamond(int x, int y, int size)
        {
            var offset = _getOffset(size);
            var a = _getCellHeight(x, y - size, size);
            var b = _getCellHeight(x, y + size, size);
            var c = _getCellHeight(x - size, y, size);
            var d = _getCellHeight(x + size, y, size);
            var average = (a + b + c + d) / 4;
            Terra[x, y] = average + offset;
        }

        #endregion

        #region Additional methods

        /// <summary>
        /// Get random offset. Value depends on current step of divide.
        /// </summary>
        /// <param name="stepSize"></param>
        /// <returns></returns>
        private float _getOffset(int stepSize)
        {
            var offset = (float)stepSize / Config.Size * Config.Random.Next(-Config.Size, Config.Size);
            var sign = offset < 0 ? -1 : 1;
            return sign * (float)Math.Pow(Math.Abs(offset), 1 / Math.Sqrt(Config.Persistence));
        }

        /// <summary>
        /// Getting height of cell by indexes. Random if cell out of map.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stepSize"></param>
        /// <returns></returns>
        private float _getCellHeight(int x, int y, int stepSize = 0)
        {
            if (x < 0 || y < 0 || x >= Config.Size || y >= Config.Size)
                return _getOffset(stepSize);
            return Terra[x, y];
        }

        #endregion

    }
}
