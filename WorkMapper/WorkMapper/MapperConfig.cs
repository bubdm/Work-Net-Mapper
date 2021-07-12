//using System;
//using WorkMapper.Expressions;

//namespace WorkMapper
//{
//    public sealed class MapperConfig
//    {
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
//    }
//}
