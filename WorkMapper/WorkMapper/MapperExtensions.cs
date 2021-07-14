namespace WorkMapper
{
    using Smart.Converter;
    using Smart.Reflection;

    using WorkMapper.Components;
    using WorkMapper.Expressions;
    using WorkMapper.Handlers;

    public static class MapperExtensions
    {
        //--------------------------------------------------------------------------------
        // Config
        //--------------------------------------------------------------------------------

        public static Mapper ToMapper(this MapperConfig config) => new(config);

        public static MapperConfig AddDefaultMapper(this MapperConfig config)
        {
            config.MissingHandlers.Add(new DefaultMapperHandler());
            return config;
        }

        //--------------------------------------------------------------------------------
        // Expression
        //--------------------------------------------------------------------------------

        public static IDefaultExpression UseFactoryResolver(this IDefaultExpression expression, IDelegateFactory delegateFactory)
        {
            expression.FactoryUsing(new DefaultFactoryResolver(delegateFactory));
            return expression;
        }

        public static IDefaultExpression UseFactoryResolver(this IDefaultExpression expression, IObjectConverter objectConverter)
        {
            expression.ConvertUsing(new DefaultConverterResolver(objectConverter));
            return expression;
        }
    }
}
