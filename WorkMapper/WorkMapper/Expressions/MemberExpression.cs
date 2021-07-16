namespace WorkMapper.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using WorkMapper.Components;
    using WorkMapper.Functions;

    internal class MemberExpression<TSource, TDestination, TMember> : IMemberExpression<TSource, TDestination, TMember>
    {
        public MemberInfo DestinationMember { get; }

        public MemberExpression(MemberInfo destinationMember)
        {
            DestinationMember = destinationMember;
        }

        public IMemberExpression<TSource, TDestination, TMember> Ignore()
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> Nested()
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> Order(int order)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, ResolutionContext, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, TDestination, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> Condition(Func<TSource, TDestination, ResolutionContext, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom<TSourceMember>(Expression<Func<TSource, ResolutionContext, TSourceMember>> expression)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom(IValueResolver<TSource, TDestination, TMember> resolver)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom<TValueResolver>() where TValueResolver : IValueResolver<TSource, TDestination, TMember>
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> MapFrom(string name)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> Const(TMember value)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> NullIf(TMember value)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing(IConverterResolver resolver)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember>(Func<TSourceMember, TMember> converter)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember>(Func<TSourceMember, ResolutionContext, TMember> converter)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember>(IValueConverter<TSourceMember, TMember> converter)
        {
            throw new NotImplementedException();
        }

        public IMemberExpression<TSource, TDestination, TMember> ConvertUsing<TSourceMember, TValueConverter>() where TValueConverter : IValueConverter<TSourceMember, TMember>
        {
            throw new NotImplementedException();
        }
    }
}
