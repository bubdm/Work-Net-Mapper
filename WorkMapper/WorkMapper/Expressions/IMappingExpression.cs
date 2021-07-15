namespace WorkMapper.Expressions
{
    using System;
    using System.Linq.Expressions;

    using WorkMapper.Functions;

    public interface IMappingExpression<TSource, TDestination>
    {
        //--------------------------------------------------------------------------------
        //  Factory
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination> FactoryUsing(Func<TDestination> factory);

        IMappingExpression<TSource, TDestination> FactoryUsing(Func<TSource, TDestination> factory);

        IMappingExpression<TSource, TDestination> FactoryUsing(IObjectFactory factory);

        IMappingExpression<TSource, TDestination> FactoryUsing(IObjectFactory<TSource> factory);

        // Type

        IMappingExpression<TSource, TDestination> FactoryUsing<TObjectFactory>();

        //--------------------------------------------------------------------------------
        // Pre/Post process
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> action);

        IMappingExpression<TSource, TDestination> BeforeMap(IMappingAction<TSource, TDestination> action);

        IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> action);

        IMappingExpression<TSource, TDestination> AfterMap(IMappingAction<TSource, TDestination> action);

        // Type

        IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>();

        IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>();

        //--------------------------------------------------------------------------------
        // Match
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination> MatchMember(Func<string, string> function);

        //--------------------------------------------------------------------------------
        // Member
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> expression, Action<IMemberExpression<TSource, TDestination, TMember>> option);
    }

    public interface IMappingExpression<TSource, TDestination, TContext>
    {
        //--------------------------------------------------------------------------------
        //  Factory
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination, TContext> FactoryUsing(Func<TDestination> factory);

        IMappingExpression<TSource, TDestination, TContext> FactoryUsing(Func<TSource, TDestination> factory);

        IMappingExpression<TSource, TDestination, TContext> FactoryUsing(IObjectFactory<TSource> factory);

        // With context

        IMappingExpression<TSource, TDestination, TContext> FactoryUsing(Func<TContext, TDestination> factory);

        IMappingExpression<TSource, TDestination, TContext> FactoryUsing(Func<TSource, TContext, TDestination> factory);

        IMappingExpression<TSource, TDestination, TContext> FactoryUsing(IObjectFactory<TSource, TContext> factory);

        // Type

        IMappingExpression<TSource, TDestination, TContext> FactoryUsing<TObjectFactory>();

        //--------------------------------------------------------------------------------
        // Pre/Post process
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination, TContext> BeforeMap(Action<TSource, TDestination> action);

        IMappingExpression<TSource, TDestination, TContext> BeforeMap(IMappingAction<TSource, TDestination> action);

        IMappingExpression<TSource, TDestination, TContext> AfterMap(Action<TSource, TDestination> action);

        IMappingExpression<TSource, TDestination, TContext> AfterMap(IMappingAction<TSource, TDestination> action);

        // With context

        IMappingExpression<TSource, TDestination, TContext> BeforeMap(Action<TSource, TDestination, TContext> action);

        IMappingExpression<TSource, TDestination, TContext> BeforeMap(IMappingAction<TSource, TDestination, TContext> action);

        IMappingExpression<TSource, TDestination, TContext> BeforeMap<TMappingAction>(IMappingAction<TSource, TDestination, TContext> action);

        IMappingExpression<TSource, TDestination, TContext> AfterMap(Action<TSource, TDestination, TContext> action);

        IMappingExpression<TSource, TDestination, TContext> AfterMap(IMappingAction<TSource, TDestination, TContext> action);

        IMappingExpression<TSource, TDestination, TContext> AfterMap<TMappingAction>(IMappingAction<TSource, TDestination, TContext> action);

        // Type

        IMappingExpression<TSource, TDestination, TContext> BeforeMap<TMappingAction>();

        IMappingExpression<TSource, TDestination, TContext> AfterMap<TMappingAction>();

        //--------------------------------------------------------------------------------
        // Match
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination, TContext> MatchMember(Func<string, string> function);

        //--------------------------------------------------------------------------------
        // Member
        //--------------------------------------------------------------------------------

        IMappingExpression<TSource, TDestination, TContext> ForMember<TMember>(Expression<Func<TDestination, TMember>> expression, Action<IMemberExpression<TSource, TDestination, TMember>> option);

        IMappingExpression<TSource, TDestination, TContext> ForMember<TMember>(Expression<Func<TDestination, TMember>> expression, Action<IMemberExpression<TSource, TDestination, TMember, TContext>> option);
    }
}
