using System;
using System.Collections.Generic;

namespace Structure
{
    public class Map
    {

        public IDictionary<Type, Layer> Layers;

        public void AddLayer(Layer layer)
        {
            var type = layer.GetType();
            Layers.Add(type, layer);
        }

        public TLayer GetLayer<TLayer>()
            where TLayer : Layer
        {
            return (TLayer)Layers[typeof(TLayer)];
        }

        public bool HasLayer<TLayer>()
            where TLayer : Layer
        {
            return Layers.Keys.Contains(typeof(TLayer));
        }

    }
}
