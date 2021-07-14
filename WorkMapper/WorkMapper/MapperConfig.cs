//using System;
//using WorkMapper.Expressions;

using System;
using System.Collections.Generic;

using Smart.Reflection;

using WorkMapper.Expressions;
using WorkMapper.Handlers;
using WorkMapper.Mappers;
using WorkMapper.Options;

namespace WorkMapper
{
    public sealed class MapperConfig
    {
        internal DefaultOption DefaultOption { get; } = new();

        // TODO

        // TODO CreateにしてDefaultをくわせる？
        internal IMapperFactory MapperFactory => ReflectionHelper.IsCodegenAllowed
            ? EmitMapperFactory.Instance
            : ReflectionMapperFactory.Instance;

        internal IMissingHandler[] MissingHandlers => Array.Empty<IMissingHandler>();

        internal IEnumerable<MapperEntry> MapperOptions => new List<MapperEntry>();


//        // (Default)ObjectFactory
//        // (Default)ConverterFactory
//        // (ValueHolderSupport!) Fallback?, ILOnly?, Expression自動？

//        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
//        {
//            throw new NotImplementedException();
//        }

        public MapperConfig Default(Action<IDefaultExpression> option)
        {
            option(new DefaultExpression(DefaultOption));
            return this;
        }

//        public MapperConfig MemberDefault<TMember>(Action<ITypeDefaultExpression<TMember>> option)
//        {
//            // TODO Default
//            return this;
//        }
    }
}
