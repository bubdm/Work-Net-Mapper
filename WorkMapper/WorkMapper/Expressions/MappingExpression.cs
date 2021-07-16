namespace WorkMapper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    using WorkMapper.Functions;
    using WorkMapper.Options;

    internal sealed class MappingExpression<TSource, TDestination> : IMappingExpression<TSource, TDestination>
    {
        private readonly MappingOption option;

        // TODO public instance can write only
        private Dictionary<PropertyInfo, MemberOption>? memberOptions;

        public MappingExpression(MappingOption option)
        {
            this.option = option;
        }

        //--------------------------------------------------------------------------------
        //  Factory
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> FactoryUsing(Func<TDestination> factory)
        {
            option.SetFactory(factory);
            return this;
        }

        public IMappingExpression<TSource, TDestination> FactoryUsing(Func<TSource, TDestination> factory)
        {
            option.SetFactory(factory);
            return this;
        }

        public IMappingExpression<TSource, TDestination> FactoryUsing(IObjectFactory<TDestination> factory)
        {
            option.SetFactory(factory);
            return this;
        }

        public IMappingExpression<TSource, TDestination> FactoryUsing<TObjectFactory>()
            where TObjectFactory : IObjectFactory<TDestination>
        {
            option.SetFactory<TDestination, TObjectFactory>();
            return this;
        }

        //--------------------------------------------------------------------------------
        // Pre/Post process
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> action)
        {
            option.AddBeforeMap(action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> BeforeMap(IMappingAction<TSource, TDestination> action)
        {
            option.AddBeforeMap(action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination, ResolutionContext> action)
        {
            option.AddBeforeMap(action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
        {
            option.AddBeforeMap<TSource, TDestination, TMappingAction>();
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> action)
        {
            option.AddAfterMap(action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap(IMappingAction<TSource, TDestination> action)
        {
            option.AddAfterMap(action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination, ResolutionContext> action)
        {
            option.AddAfterMap(action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>()
            where TMappingAction : IMappingAction<TSource, TDestination>
        {
            option.AddAfterMap<TSource, TDestination, TMappingAction>();
            return this;
        }

        //        //--------------------------------------------------------------------------------
        //        // Match
        //        //--------------------------------------------------------------------------------

        //        public IMappingExpression<TSource, TDestination> MatchMember(Func<string, string> function)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        //--------------------------------------------------------------------------------
        //        // All members
        //        //--------------------------------------------------------------------------------

        //        public void ForAllMembers(Action<IMemberExpression<TSource, TDestination, object>> option)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        //--------------------------------------------------------------------------------
        //        // Member
        //        //--------------------------------------------------------------------------------

        //        public IMappingExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> expression, Action<IMemberExpression<TSource, TDestination, TMember>> option)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public IMappingExpression<TSource, TDestination> ForMember(string name, Action<IMemberExpression<TSource, TDestination, object>> option)
        //        {
        //            throw new NotImplementedException();
        //        }

        public IMappingExpression<TSource, TDestination> MatchMember(Func<string, string> function)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> expression, Action<IMemberExpression<TSource, TDestination, TMember>> option)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Default
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> Default(Action<IMappingDefaultExpression> action)
        {
            action(new MappingDefaultExpression(option));
            return this;
        }
    }
}
