using System.Collections.Generic;

namespace MapMaker.Ravine
{
    public interface IPropagator
    {

        IDictionary<WaterDrop, Vector> Propagate(double[,] map, IDictionary<WaterDrop, Vector> drops);

    }
}