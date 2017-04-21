using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Generators
{
    public class DiamondSquare : GeneratorAlgorithm<DiamondSquareConfig>
    {

        public DiamondSquare(DiamondSquareConfig config) : base(config)
        {
        }

        public override async Task<float[,]> Generate()
        {
            _terra = new float[_config.Size, _config.Size];

            var last = _config.Size - 1;
            _terra[0, 0] = await _getOffset(_config.Size);
            _terra[0, last] = await _getOffset(_config.Size);
            _terra[last, 0] = await _getOffset(_config.Size);
            _terra[last, last] = await _getOffset(_config.Size);

            _divide(_config.Size);

            return _terra;
        }

        private async void _divide(int stepSize)
        {
            var half = stepSize / 2;

            if (half < 1)
                return;

            for (var x = half; x < _config.Size; x += stepSize)
            {
                for (var y = half; y < _config.Size; y += stepSize)
                {
                    await _square(x, y, half, await _getOffset(stepSize));
                }
            }

            _divide(half);
        }

        private async Task _square(int x, int y, int size, float offset)
        {
            var a = await _getCellHeight(x - size, y - size, size);
            var b = await _getCellHeight(x + size, y + size, size);
            var c = await _getCellHeight(x - size, y + size, size);
            var d = await _getCellHeight(x + size, y - size, size);
            var average = (a + b + c + d) / 4;
            _terra[x, y] = average + offset;
            await _diamond(x, y - size, size);
            await _diamond(x - size, y, size);
            await _diamond(x, y + size, size);
            await _diamond(x + size, y, size);
        }

        private async Task _diamond(int x, int y, int size)
        {
            var offset = await _getOffset(size);
            var a = await _getCellHeight(x, y - size, size);
            var b = await _getCellHeight(x, y + size, size);
            var c = await _getCellHeight(x - size, y, size);
            var d = await _getCellHeight(x + size, y, size);
            var average = (a + b + c + d) / 4;
            _terra[x, y] = average + offset;
        }

        /// <summary>
        /// Get random offset. Value depends on current step of divide.
        /// </summary>
        /// <param name="stepSize"></param>
        /// <returns></returns>
        private async Task<float> _getOffset(int stepSize)
        {
            return await Task.Run(() =>
             {
                 var offset = stepSize / _config.Size * _config.Random.Next(-_config.Size, _config.Size);
                 var sign = offset < 0 ? -1 : 1;
                 return sign * (float)Math.Pow(Math.Abs(offset), 1 / Math.Sqrt(_config.Persistence));
             });

        }

        /// <summary>
        /// Getting height of cell by indexes. Random if cell out of map.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stepSize"></param>
        /// <returns></returns>
        private async Task<float> _getCellHeight(int x, int y, int stepSize = 0)
        {
            if (x < 0 || y < 0 || x >= _config.Size || y >= _config.Size)
                return await _getOffset(stepSize);
            return _terra[x, y];
        }

    }
}
