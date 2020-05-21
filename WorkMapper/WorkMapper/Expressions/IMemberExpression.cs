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

        // TODO どちらにする？
        void MapFrom<TSourceMember>(Func<TSource, TSourceMember> function);

        void MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression);

        void MapFrom(string name);

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
