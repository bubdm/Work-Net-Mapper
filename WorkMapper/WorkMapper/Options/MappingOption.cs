using System.Collections.Generic;

using WorkMapper.Functions;

namespace WorkMapper.Options
{
    using System;
//    using System.Collections.Generic;

    public class MappingOption
    {
        public Type SourceType { get; }

        public Type DestinationType { get; }

        private object? factory;

        // TODO Member

//        private Dictionary<Tuple<Type, Type>, object> factories;

        private List<object>? beforeMaps;

        private List<object>? afterMaps;

        public MappingOption(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
        }

        //--------------------------------------------------------------------------------
        //  Factory
        //--------------------------------------------------------------------------------

        public void SetFactory<TDestination>(Func<TDestination> value)
        {
            factory = value;
        }

        public void SetFactory<TSource, TDestination>(Func<TSource, TDestination> value)
        {
            factory = value;
        }

        public void SetFactory<TDestination>(IObjectFactory<TDestination> value)
        {
            factory = value;
        }

        public void SetFactory<TDestination, TObjectFactory>()
            where TObjectFactory : IObjectFactory<TDestination>
        {
            factory = typeof(TObjectFactory);
        }


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

//        public bool TryGetBeforeMaps(Tuple<Type, Type> pair, out List<object> values)
//        {
//            if ((beforeMaps is not null) && beforeMaps.TryGetValue(pair, out values))
//            {
//                return true;
//            }

//            values = null;
//            return false;
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

        //--------------------------------------------------------------------------------
        //  Internal
        //--------------------------------------------------------------------------------

        internal object? GetFactory() => factory;
    }
}
