namespace WorkMapper.Options
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using WorkMapper.Components;
    using WorkMapper.Functions;

    public sealed class DefaultOption
    {
        private IFactoryResolver? factoryResolver;

        private Dictionary<Type, object>? factories;

        private IConverterResolver? converterResolver;

        private Dictionary<Tuple<Type, Type>, object>? converters;

        private Dictionary<Type, object?>? constValues;

        private Dictionary<Type, object?>? nullIfValues;

        private HashSet<Type>? nullIgnores;

        //--------------------------------------------------------------------------------
        // Factory
        //--------------------------------------------------------------------------------

        public void SetFactoryResolver(IFactoryResolver value)
        {
            factoryResolver = value;
        }

        public void SetFactory<TDestination>(Func<TDestination> value)
        {
            factories ??= new Dictionary<Type, object>();
            factories[value.GetType().GetGenericArguments()[0]] = value;
        }

        //--------------------------------------------------------------------------------
        // Converter
        //--------------------------------------------------------------------------------

        public void SetConverterResolver(IConverterResolver value)
        {
            converterResolver = value;
        }

        public void SetConverter<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter) =>
            SetConverter(new Tuple<Type, Type>(typeof(TSourceMember), typeof(TDestinationMember)), converter);

        public void SetConverter<TSourceMember, TDestinationMember, TContext>(Func<TSourceMember, TContext, TDestinationMember> converter) =>
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
        // Internal
        //--------------------------------------------------------------------------------

        internal IFactoryResolver? GetFactoryResolver() => factoryResolver;

        internal object? GetFactory(Type type)
        {
            if ((factories is not null) && factories.TryGetValue(type, out var value))
            {
                return value;
            }

            return null;
        }

        internal IConverterResolver? GetConverterResolver() => converterResolver;

        internal bool TryGetConverter(Tuple<Type, Type> pair, [NotNullWhen(true)] out object? value)
        {
            if ((converters is not null) && converters.TryGetValue(pair, out value))
            {
                return true;
            }

            value = null;
            return false;
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
    }
}
