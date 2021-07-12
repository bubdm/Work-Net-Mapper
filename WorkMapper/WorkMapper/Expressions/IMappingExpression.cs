//namespace WorkMapper.Expressions
//{
//    using System;
//    using System.Linq.Expressions;
//    using System.Reflection;

//    public interface IMappingExpression<TSource, TDestination>
//    {
//        //--------------------------------------------------------------------------------
//        //  Factory
//        //--------------------------------------------------------------------------------

//        IMappingExpression<TSource, TDestination> FactoryUsing(Func<TDestination> factory);

//        IMappingExpression<TSource, TDestination> FactoryUsing(Func<TDestination, object> factory);

//        IMappingExpression<TSource, TDestination> FactoryUsing(Func<TSource, TDestination> factory);

//        IMappingExpression<TSource, TDestination> FactoryUsing(Func<TSource, TDestination, object> factory);

//        //--------------------------------------------------------------------------------
//        // Pre/Post process
//        //--------------------------------------------------------------------------------

//        IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> action);

//        IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination, object> action);

//        IMappingExpression<TSource, TDestination> BeforeMap(IMappingAction<TSource, TDestination> action);

//        IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>()
//            where TMappingAction : IMappingAction<TSource, TDestination>;

//        IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> action);

//        IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination, object> action);

//        IMappingExpression<TSource, TDestination> AfterMap(IMappingAction<TSource, TDestination> action);

//        IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>()
//            where TMappingAction : IMappingAction<TSource, TDestination>;

//        //--------------------------------------------------------------------------------
//        // Match
//        //--------------------------------------------------------------------------------

//        IMappingExpression<TSource, TDestination> MatchMember(Func<string, string> function);

//        //--------------------------------------------------------------------------------
//        // Include/Exclude
//        //--------------------------------------------------------------------------------

//        IMappingExpression<TSource, TDestination> IncludeMember(params string[] names);

//        IMappingExpression<TSource, TDestination> IncludeMembers(params Expression<Func<TSource, object>>[] expressions);

//        IMappingExpression<TSource, TDestination> IncludeMember(Func<MemberInfo, bool> filter);

//        IMappingExpression<TSource, TDestination> ExcludeMember(params string[] names);

//        IMappingExpression<TSource, TDestination> ExcludeMember(Func<MemberInfo, bool> filter);

//        IMappingExpression<TSource, TDestination> ExcludeMembers(params Expression<Func<TSource, object>>[] expressions);

//        //--------------------------------------------------------------------------------
//        // All members
//        //--------------------------------------------------------------------------------

//        void ForAllMembers(Action<IMemberExpression<TSource, TDestination, object>> option);

//        //--------------------------------------------------------------------------------
//        // Member
//        //--------------------------------------------------------------------------------

//        IMappingExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> expression, Action<IMemberExpression<TSource, TDestination, TMember>> option);

//        IMappingExpression<TSource, TDestination> ForMember(string name, Action<IMemberExpression<TSource, TDestination, object>> option);

//        //--------------------------------------------------------------------------------
//        // Default
//        //--------------------------------------------------------------------------------

//        IMappingExpression<TSource, TDestination> Default(Action<IDefaultExpression> option);

//        IMappingExpression<TSource, TDestination> MemberDefault<TMember>(Action<ITypeDefaultExpression<TMember>> option);
//    }
//}
