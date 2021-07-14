namespace WorkMapper.Options
{
    using System;
//    using System.Collections.Generic;

    public class MappingOption
    {
        public Type SourceType { get; }

        public Type DestinationType { get; }

        public Type? ContextType { get; set; }

//        private Dictionary<Tuple<Type, Type>, object> factories;

//        private Dictionary<Tuple<Type, Type>, List<object>> beforeMaps;

//        private Dictionary<Tuple<Type, Type>, List<object>> afterMaps;

        public MappingOption(Type sourceType, Type destinationType, Type? contextType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
            ContextType = contextType;
        }

//        //--------------------------------------------------------------------------------
//        //  Factory
//        //--------------------------------------------------------------------------------

//        public void SetFactory(Tuple<Type, Type> pair, object value)
//        {
//            factories ??= new Dictionary<Tuple<Type, Type>, object>();
//            factories[pair] = value;
//        }

//        public bool TryGetFactory(Tuple<Type, Type> pair, out object value)
//        {
//            if ((factories is not null) && factories.TryGetValue(pair, out value))
//            {
//                return true;
//            }

//            value = null;
//            return false;
//        }

//        //--------------------------------------------------------------------------------
//        // Pre/Post process
//        //--------------------------------------------------------------------------------

//        public void AddBeforeMap(Tuple<Type, Type> pair, object value)
//        {
//            beforeMaps ??= new Dictionary<Tuple<Type, Type>, List<object>>();
//            if (!beforeMaps.TryGetValue(pair, out var list))
//            {
//                list = new List<object>();
//                beforeMaps[pair] = list;
//            }

//            list.Add(value);
//        }

//        public bool TryGetBeforeMaps(Tuple<Type, Type> pair, out List<object> values)
//        {
//            if ((beforeMaps is not null) && beforeMaps.TryGetValue(pair, out values))
//            {
//                return true;
//            }

//            values = null;
//            return false;
//        }

//        public void AddAfterMap(Tuple<Type, Type> pair, object value)
//        {
//            afterMaps ??= new Dictionary<Tuple<Type, Type>, List<object>>();
//            if (!afterMaps.TryGetValue(pair, out var list))
//            {
//                list = new List<object>();
//                afterMaps[pair] = list;
//            }

//            list.Add(value);
//        }

//        public bool TryGetAfterMaps(Tuple<Type, Type> pair, out List<object> values)
//        {
//            if ((afterMaps is not null) && afterMaps.TryGetValue(pair, out values))
//            {
//                return true;
//            }

//            values = null;
//            return false;
//        }
    }
}
