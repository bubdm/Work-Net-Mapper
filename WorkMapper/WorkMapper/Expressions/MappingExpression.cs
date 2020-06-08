namespace WorkMapper.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using WorkMapper.Metadata;

    public class MappingExpression<TSource, TDestination> : IMappingExpression<TSource, TDestination>
    {
        private readonly MapperEntry entry;

        public MappingExpression(MapperEntry entry)
        {
            this.entry = entry;
        }

        //--------------------------------------------------------------------------------
        //  Factory
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> FactoryUsing(Func<TDestination> factory)
        {
            entry.SetFactory(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), factory);
            return this;
        }

        public IMappingExpression<TSource, TDestination> FactoryUsing(Func<TDestination, object> factory)
        {
            entry.SetFactory(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), factory);
            return this;
        }

        public IMappingExpression<TSource, TDestination> FactoryUsing(Func<TSource, TDestination> factory)
        {
            entry.SetFactory(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), factory);
            return this;
        }

        public IMappingExpression<TSource, TDestination> FactoryUsing(Func<TSource, TDestination, object> factory)
        {
            entry.SetFactory(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), factory);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Pre/Post process
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> action)
        {
            entry.AddBeforeMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination, object> action)
        {
            entry.AddBeforeMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> BeforeMap(IMappingAction<TSource, TDestination> action)
        {
            entry.AddBeforeMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
        {
            entry.AddBeforeMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), typeof(TMappingAction));
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> action)
        {
            entry.AddAfterMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination, object> action)
        {
            entry.AddAfterMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap(IMappingAction<TSource, TDestination> action)
        {
            entry.AddAfterMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), action);
            return this;
        }

        public IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
        {
            entry.AddAfterMap(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)), typeof(TMappingAction));
            return this;
        }

        //--------------------------------------------------------------------------------
        // Match
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> MatchMember(Func<string, string> function)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Include/Exclude
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> IncludeMember(params string[] names)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> IncludeMembers(params Expression<Func<TSource, object>>[] expressions)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> IncludeMember(Func<MemberInfo, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> ExcludeMember(params string[] names)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> ExcludeMember(Func<MemberInfo, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> ExcludeMembers(params Expression<Func<TSource, object>>[] expressions)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // All members
        //--------------------------------------------------------------------------------

        public void ForAllMembers(Action<IMemberExpression<TSource, TDestination, object>> option)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Member
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> expression, Action<IMemberExpression<TSource, TDestination, TMember>> option)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> ForMember(string name, Action<IMemberExpression<TSource, TDestination, object>> option)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Default
        //--------------------------------------------------------------------------------

        public IMappingExpression<TSource, TDestination> Default(Action<IDefaultExpression> option)
        {
            throw new NotImplementedException();
        }

        public IMappingExpression<TSource, TDestination> MemberDefault<TMember>(Action<ITypeDefaultExpression<TMember>> option)
        {
            throw new NotImplementedException();
        }
    }
}
