using System;
using System.Collections.Generic;

namespace Structure
{
    public class Map
    {

        private readonly Dictionary<Type, object> _layers = new Dictionary<Type, object>();

        /// <summary>
        /// Add layer to map. Second typed parameter is a type of layer cell
        /// </summary>
        /// <typeparam name="TLayer"></typeparam>
        /// <typeparam name="TCell"></typeparam>
        /// <param name="layer"></param>
        public void AddLayer<TLayer, TCell>(TLayer layer) 
            where TLayer : Layer<TCell>
        {
            _layers.Add(typeof(TLayer), layer);
        }

        /// <summary>
        /// Check if there is a layer in the map
        /// </summary>
        /// <typeparam name="TLayer"></typeparam>
        /// <typeparam name="TCell"></typeparam>
        /// <returns></returns>
        public bool HasLayer<TLayer, TCell>()
            where TLayer : Layer<TCell>
        {
            return _layers.ContainsKey(typeof(TLayer));
        }

        /// <summary>
        /// Get layer by type (type is key)
        /// </summary>
        /// <typeparam name="TLayer"></typeparam>
        /// <typeparam name="TCell"></typeparam>
        /// <returns></returns>
        public TLayer GetLayer<TLayer, TCell>() 
            where TLayer : Layer<TCell>
        {
            return _layers[typeof(TLayer)] as TLayer;
        }

    }
}
