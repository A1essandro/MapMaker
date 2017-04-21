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

        abstract public Task<float[,]> Generate();

    }

}
