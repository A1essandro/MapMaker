using System.Threading.Tasks;

namespace Generators
{

    public abstract class GeneratorAlgorithm<TConfig>
        where TConfig : class
    {

        protected float[,] Terra;
        protected TConfig Config;

        protected GeneratorAlgorithm(TConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// Async implementation of Generate
        /// </summary>
        /// <returns>Task will returns double array of float</returns>
        public async Task<float[,]> GenerateAsync()
        {
            await Task.Run(() =>
            {
                Generate();
            });

            return Terra;
        }

        /// <summary>
        /// Non-async implementation of Generate
        /// </summary>
        /// <returns>Double array of float</returns>
        public abstract float[,] Generate();

    }

}
