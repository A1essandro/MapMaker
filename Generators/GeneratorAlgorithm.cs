using System.Threading.Tasks;

namespace Generators
{

    public abstract class GeneratorAlgorithm<TConfig>
        where TConfig : class
    {

        protected float[,] _terra;
        protected TConfig _config;

        public GeneratorAlgorithm(TConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Async implementation of Generate
        /// </summary>
        /// <returns>Task will returns double array of float</returns>
        abstract public Task<float[,]> GenerateAsync();

        /// <summary>
        /// Non-async implementation of Generate
        /// </summary>
        /// <returns>Double array of float</returns>
        public float[,] Generate()
        {
            return GenerateAsync().GetAwaiter().GetResult();
        }

    }

}
