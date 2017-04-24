using System;
using System.Collections.Generic;

namespace Structure
{
    public class Map
    {

        private Dictionary<Type, object> _layers = new Dictionary<Type, object>();

        public void AddLayer<TLayer, TCell>(TLayer layer) 
            where TLayer : Layer<TCell>
        {
            _layers.Add(typeof(TLayer), layer);
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
