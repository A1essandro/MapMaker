using System;
using System.Collections.Generic;

namespace Structure
{
    public class Map
    {

        public Dictionary<Type, object> _layers;

        public void AddLayer<TLayer, TCell>(TLayer layer) 
            where TLayer : Layer<TCell>
        {
            Type key = typeof(TLayer);
            _layers.Add(key, layer);
        }

        public bool HasLayer<TLayer, TCell>()
            where TLayer : Layer<TCell>
        {
            return _layers.ContainsKey(typeof(TLayer));
        }

        public TLayer GetLayer<TLayer, TCell>() 
            where TLayer : Layer<TCell>
        {
            return _layers[typeof(TLayer)] as TLayer;
        }

    }
}
