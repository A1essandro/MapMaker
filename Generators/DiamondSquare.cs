using System;

namespace Generators
{
    public class DiamondSquare : GeneratorAlgorithm<DiamondSquareConfig>
    {
        private Random _random;

        public DiamondSquare(DiamondSquareConfig config) : base(config)
        {
            _random = new Random();
        }

        public override float[,] Generate()
        {
            _terra = new float[_config.Size, _config.Size];

            var last = _config.Size - 1;
            _terra[0, 0] = _getOffset(_config.Size);
            _terra[0, last] = _getOffset(_config.Size);
            _terra[last, 0] = _getOffset(_config.Size);
            _terra[last, last] = _getOffset(_config.Size);

            _divide(_config.Size);

            return _terra;
        }

        private void _divide(int stepSize)
        {
            var half = stepSize / 2;

            if (half < 1)
                return;

            for (var x = half; x < _config.Size; x += stepSize)
            {
                for (var y = half; y < _config.Size; y += stepSize)
                {
                    _square(x, y, half, _getOffset(stepSize));
                }
            }

            _divide(half);
        }

        private void _square(int x, int y, int size, float offset)
        {
            var a = _getCellHeight(x - size, y - size, size);
            var b = _getCellHeight(x + size, y + size, size);
            var c = _getCellHeight(x - size, y + size, size);
            var d = _getCellHeight(x + size, y - size, size);
            var average = (a + b + c + d) / 4;
            _terra[x, y] = average + offset;
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
            _terra[x, y] = average + offset;
        }

        /// <summary>
        /// Get random offset. Value depends on current step of divide.
        /// </summary>
        /// <param name="stepSize"></param>
        /// <returns></returns>
        private float _getOffset(int stepSize)
        {
            var offset = stepSize / _config.Size * _random.Next(-_config.Size, _config.Size);
            var sign = offset < 0 ? -1 : 1;
            return sign * (float)Math.Pow(Math.Abs(offset), 1 / Math.Sqrt(_config.Persistence));
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
            if (x < 0 || y < 0 || x >= _config.Size || y >= _config.Size)
                return _getOffset(stepSize);
            return _terra[x, y];
        }

    }
}
