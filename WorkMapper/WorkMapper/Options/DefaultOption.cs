namespace WorkMapper.Options
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using WorkMapper.Expressions;

    public sealed class DefaultOption
    {
        private Dictionary<Type, object>? factories;

        private IObjectFactory? factory;

        private Dictionary<Type, object?>? nullIfValues;

        private HashSet<Type>? nullIgnores;

        private Dictionary<Type, object?>? constValues;

        private Dictionary<Tuple<Type, Type>, object>? converters;

        private Dictionary<Tuple<Type, Type>, Type>? converterTypes;

        //--------------------------------------------------------------------------------
        // Factory
        //--------------------------------------------------------------------------------

        public void SetFactory<TDestination>(Func<TDestination> value)
        {
            factories ??= new Dictionary<Type, object>();
            factories[value.GetType().GetGenericArguments()[0]] = value;
        }

        public void SetFactory(IObjectFactory value)
        {
            this.factory = value;
        }

        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        public void SetNullIfValue<TMember>(TMember value)
        {
            nullIfValues ??= new Dictionary<Type, object?>();
            nullIfValues[typeof(TMember)] = value;
        }

        public void SetNullIgnore(Type type)
        {
            nullIgnores ??= new HashSet<Type>();
            nullIgnores.Add(type);
        }

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        public void SetConstValue<TMember>(TMember value)
        {
            constValues ??= new Dictionary<Type, object?>();
            constValues[typeof(TMember)] = value;
        }

        //--------------------------------------------------------------------------------
        // Converter
        //--------------------------------------------------------------------------------

        public void SetConverter<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter) =>
            SetConverter(new Tuple<Type, Type>(typeof(TSourceMember), typeof(TDestinationMember)), converter);

        public void SetConverter<TSourceMember, TDestinationMember, TContext>(Func<TSourceMember, TDestinationMember, TContext> converter) =>
            SetConverter(new Tuple<Type, Type>(typeof(TSourceMember), typeof(TDestinationMember)), converter);

        public void SetConverter<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter) =>
            SetConverter(new Tuple<Type, Type>(typeof(TSourceMember), typeof(TDestinationMember)), converter);

        public void SetConverter<TSourceMember, TDestinationMember, TContext>(IValueConverter<TSourceMember, TDestinationMember, TContext> converter) =>
            SetConverter(new Tuple<Type, Type>(typeof(TSourceMember), typeof(TDestinationMember)), converter);

        private void SetConverter(Tuple<Type, Type> pair, object value)
        {
            converters ??= new Dictionary<Tuple<Type, Type>, object>();
            converters[pair] = value;
        }

        //--------------------------------------------------------------------------------
        // Internal
        //--------------------------------------------------------------------------------

        internal object? GetFactory(Type type)
        {
            if ((factories is not null) && factories.TryGetValue(type, out var value))
            {
                return value;
            }

            return null;
        }

        internal IObjectFactory? GetFactory() => factory;

        internal bool TryGetNullIfValue(Type type, out object? value)
        {
            if ((nullIfValues is not null) && nullIfValues.TryGetValue(type, out value))
            {
                return true;
            }

            value = null;
            return false;
        }

        internal bool IsNullIgnore(Type type)
        {
            return (nullIgnores is not null) && nullIgnores.Contains(type);
        }

        internal bool TryGetConstValue(Type type, out object? value)
        {
            if ((constValues is not null) && constValues.TryGetValue(type, out value))
            {
                return true;
            }

            value = null;
            return false;
        }

        internal bool TryGetConverter(Tuple<Type, Type> pair, [NotNullWhen(true)] out object? value)
        {
            if ((converters is not null) && converters.TryGetValue(pair, out value))
            {
                return true;
            }

            value = null;
            return false;
        }

        internal bool TryGetConverterType(Tuple<Type, Type> pair, [NotNullWhen(true)] out Type? value)
        {
            if ((converterTypes is not null) && converterTypes.TryGetValue(pair, out value))
            {
                return true;
            }

            value = null;
            return false;
        }
    }
}
