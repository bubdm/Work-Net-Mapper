namespace WorkMapper.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using WorkMapper.Components;
    using WorkMapper.Functions;
    using WorkMapper.Options;

    internal class MemberExpression<TSource, TDestination, TMember> : IMemberExpression<TSource, TDestination, TMember>
    {
        private readonly MemberOption option;

        public PropertyInfo DestinationMember { get; }

        public MemberExpression(PropertyInfo property, MemberOption option)
        {
            DestinationMember = property;
            this.option = option;
        }

        //--------------------------------------------------------------------------------
        // Ignore
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> Ignore()
        {
            option.SetIgnore();
            return this;
        }

        //--------------------------------------------------------------------------------
        // Nested
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> Nested()
        {
            option.SetNested();
            return this;
        }

        //--------------------------------------------------------------------------------
        // Order
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> Order(int order)
        {
            option.SetOrder(order);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Condition
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, bool> condition)
        {
            option.SetCondition(condition);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, ResolutionContext, bool> condition)
        {
            option.SetCondition(condition);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, TDestination, bool> condition)
        {
            option.SetCondition(condition);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, TDestination, ResolutionContext, bool> condition)
        {
            option.SetCondition(condition);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(IMemberCondition<TSource, TDestination> condition)
        {
            option.SetCondition(condition);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition<TMemberCondition>() where TMemberCondition : IMemberCondition<TSource, TDestination>
        {
            option.SetCondition<TMemberCondition>();
            return this;
        }

        //--------------------------------------------------------------------------------
        // MapFrom
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression)
        {
            option.SetMapFrom(expression);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom<TSourceMember>(Expression<Func<TSource, ResolutionContext, TSourceMember>> expression)
        {
            option.SetMapFrom(expression);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom(IValueResolver<TSource, TDestination, TMember> resolver)
        {
            option.SetMapFrom(resolver);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom<TValueResolver>()
            where TValueResolver : IValueResolver<TSource, TDestination, TMember>
        {
            option.SetMapFrom<TSource, TDestination, TMember, TValueResolver>();
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom(string sourcePath)
        {
            option.SetMapFrom(sourcePath);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Const
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> Const(TMember value)
        {
            option.SetConst(value);
            return this;
        }

        //--------------------------------------------------------------------------------
        // NullIf
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> NullIf(TMember value)
        {
            option.SetNullIf(value);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing(IConverterResolver resolver)
        {
            option.SetConverter(resolver);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember>(Func<TSourceMember, TMember> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember>(Func<TSourceMember, ResolutionContext, TMember> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember>(IValueConverter<TSourceMember, TMember> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember, TValueConverter>()
            where TValueConverter : IValueConverter<TSourceMember, TMember>
        {
            option.SetConverter<TSourceMember, TMember, TValueConverter>();
            return this;
        }
    }
}
