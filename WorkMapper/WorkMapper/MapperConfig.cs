//using System;
//using WorkMapper.Expressions;

using System;
using System.Collections.Generic;

using Smart.Reflection;

using WorkMapper.Handlers;
using WorkMapper.Mappers;
using WorkMapper.Options;

namespace WorkMapper
{
    public sealed class MapperConfig
    {
        // TODO

        // TODO CreateにしてDefaultをくわせる？
        internal IMapperFactory MapperFactory => ReflectionHelper.IsCodegenAllowed
            ? EmitMapperFactory.Instance
            : ReflectionMapperFactory.Instance;

        internal IMissingHandler[] MissingHandlers => Array.Empty<IMissingHandler>();

        internal DefaultOption DefaultOption => new DefaultOption();

        internal IEnumerable<MapperEntry> MapperOptions => new List<MapperEntry>();


//        // (Default)ObjectFactory
//        // (Default)ConverterFactory
//        // (ValueHolderSupport!) Fallback?, ILOnly?, Expression自動？

//        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
//        {
//            throw new NotImplementedException();
//        }

//        public MapperConfig Default(Action<IDefaultExpression> option)
//        {
//            // TODO Default
//            return this;
//        }

//        public MapperConfig MemberDefault<TMember>(Action<ITypeDefaultExpression<TMember>> option)
//        {
//            // TODO Default
//            return this;
//        }
    }
}
