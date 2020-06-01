namespace WorkMapper.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public interface IMemberExpression<TSource, out TDestination, in TMember>
    {
        // MemberInfo
        MemberInfo DestinationMember { get; }

        // Null

        void NullIf(TMember value);

        // Constant

        void Const(TMember value);

        // MapFrom

        void MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression);

        void MapFrom<TSourceMember>(Expression<Func<TSource, object, TSourceMember>> expression);

        void MapFrom(IValueResolver<TSource, TDestination, TMember> resolver);

        void MapFrom<TValueResolver>()
            where TValueResolver : IValueResolver<TSource, TDestination, TMember>;

        void MapFrom(Type resolverType);

        void MapFrom(string name);

        // Convert

        void ConvertUsing<TValueConverter, TSourceMember>(IValueConverter<TSourceMember, TMember> converter);

        void ConvertUsing<TValueConverter, TSourceMember>()
            where TValueConverter : IValueConverter<TSourceMember, TMember>;

        void ConvertUsing<TValueConverter, TSourceMember>(Type converterType);

        // Ignore
        void Ignore();

        // Order
        void Order(int order);

        // Condition

        void Condition(Func<TSource, bool> condition);

        void Condition(Func<TSource, object, bool> condition);

        void Condition(Func<TSource, TDestination, bool> condition);

        void Condition(Func<TSource, TDestination, object, bool> condition);
    }
}
