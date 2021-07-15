namespace WorkMapper.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public interface IMemberExpression<TSource, out TDestination, in TMember>
    {
        //--------------------------------------------------------------------------------
        // Info
        //--------------------------------------------------------------------------------

        MemberInfo DestinationMember { get; }

        //--------------------------------------------------------------------------------
        // Ignore
        //--------------------------------------------------------------------------------

        void Ignore();

        //--------------------------------------------------------------------------------
        // Order
        //--------------------------------------------------------------------------------

        void Order(int order);

        ////--------------------------------------------------------------------------------
        //// Condition
        ////--------------------------------------------------------------------------------

        //void Condition(Func<TSource, bool> condition);

        //void Condition(Func<TSource, object, bool> condition);

        //void Condition(Func<TSource, TDestination, bool> condition);

        //void Condition(Func<TSource, TDestination, object, bool> condition);

        ////--------------------------------------------------------------------------------
        //// MapFrom
        ////--------------------------------------------------------------------------------

        //void MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression);

        //void MapFrom<TSourceMember>(Expression<Func<TSource, object, TSourceMember>> expression);

        //void MapFrom(IValueResolver<TSource, TDestination, TMember> resolver);

        //void MapFrom<TValueResolver>()
        //    where TValueResolver : IValueResolver<TSource, TDestination, TMember>;

        //void MapFrom(string name);

        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        void NullIf(TMember value);

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        void Const(TMember value);

        ////--------------------------------------------------------------------------------
        //// Convert
        ////--------------------------------------------------------------------------------

        //void ConvertUsing<TSourceMember>(IValueConverter<TSourceMember, TMember> converter);

        //void ConvertUsing<TSourceMember, TValueConverter>()
        //    where TValueConverter : IValueConverter<TSourceMember, TMember>;
    }

    public interface IMemberExpression<TSource, out TDestination, in TMember, TContext>
    {

    }
}
