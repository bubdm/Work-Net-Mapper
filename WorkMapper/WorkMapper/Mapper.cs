namespace WorkMapper
{
    using System;

    public sealed class Mapper
    {
        private readonly TypePairHashArray mapperCache = new();

        private readonly TypePairHashArray mapperWithContextCache = new();

        private readonly TypePairProfileHashArray profileMapperCache = new();

        private readonly TypePairProfileHashArray profileMapperWithContextCache = new();


        //--------------------------------------------------------------------------------
        // Get
        //--------------------------------------------------------------------------------

        public Action<TSource, TDestination> GetMapper<TSource, TDestination>()
        {
            throw new NotImplementedException();
        }

        public Action<TSource, TContext, TDestination> GetMapper<TSource, TDestination, TContext>()
        {
            throw new NotImplementedException();
        }

        public Action<object, object> GetMapper(Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public Action<object, object, object> GetMapper(Type sourceType, Type destinationType, Type contextType)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Profile Get
        //--------------------------------------------------------------------------------

        public Action<TSource, TDestination> GetMapper<TSource, TDestination>(string profile)
        {
            throw new NotImplementedException();
        }

        public Action<TSource, TContext, TDestination> GetMapper<TSource, TDestination, TContext>(string profile)
        {
            throw new NotImplementedException();
        }

        public Action<object, object> GetMapper(string profile, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public Action<object, object, object> GetMapper(string profile, Type sourceType, Type destinationType, Type contextType)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Map
        //--------------------------------------------------------------------------------

        public TDestination Map<TDestination>(object source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TDestination, TContext>(object source, TContext context)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination, TContext>(TSource source, TContext context)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination, TContext>(TSource source, TDestination destination, TContext context)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType, object context)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, object context)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Profile Map
        //--------------------------------------------------------------------------------

        public TDestination Map<TDestination>(string profile, object source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(string profile, TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(string profile, TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TDestination, TContext>(string profile, object source, TContext context)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination, TContext>(string profile, TSource source, TContext context)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination, TContext>(string profile, TSource source, TDestination destination, TContext context)
        {
            throw new NotImplementedException();
        }

        public object Map(string profile, object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(string profile, object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(string profile, object source, Type sourceType, Type destinationType, object context)
        {
            throw new NotImplementedException();
        }

        public object Map(string profile, object source, object destination, Type sourceType, Type destinationType, object context)
        {
            throw new NotImplementedException();
        }
    }
}
