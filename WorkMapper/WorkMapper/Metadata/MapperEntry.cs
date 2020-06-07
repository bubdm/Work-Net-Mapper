
using System;
using System.Collections.Generic;

namespace WorkMapper.Metadata
{
    public class MapperEntry
    {
        private Dictionary<Tuple<Type, Type>, object> factories;

        public void SetFactory(Tuple<Type, Type> pair, object value)
        {
            factories ??= new Dictionary<Tuple<Type, Type>, object>();
            factories[pair] = value;
        }

        public bool TryGetFactory(Tuple<Type, Type> pair, out object value)
        {
            if ((factories != null) && factories.TryGetValue(pair, out value))
            {
                return true;
            }

            value = null;
            return false;
        }
    }
}
