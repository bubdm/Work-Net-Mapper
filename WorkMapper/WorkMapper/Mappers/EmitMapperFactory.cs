namespace WorkMapper.Mappers
{
    using System;

    using WorkMapper.Components;
    using WorkMapper.Options;

    internal class EmitMapperFactory : IMapperFactory
    {
        private readonly IFactoryResolver factoryResolver;

        private readonly IConverterResolver converterResolver;

        private readonly IFunctionActivator functionActivator;

        public EmitMapperFactory(
            IFactoryResolver factoryResolver,
            IConverterResolver converterResolver,
            IFunctionActivator functionActivator)
        {
            this.factoryResolver = factoryResolver;
            this.converterResolver = converterResolver;
            this.functionActivator = functionActivator;
        }

        // IConverterResolver, IFactoryResolver, IFunctionActivatorはDI

        public object Create(MapperCreateContext context)
        {
            // TODO factoryResolverを使うかどうかはオプションか？/Default/Mapper、Activatorを使う？
            // TODO コンストラクターセレクター？、Factoryと一緒にする？、デフォルトCtor前提で良い(AutoMapperはそう)

            // TODO 4パターン、作る必要があるかの確認DynamicにDynamicを食わせて遅いかとか
            //public readonly Action<TSource, TDestination> MapAction;
            //public readonly Func<TSource, TDestination> MapFunc;
            //public readonly Action<TSource, TDestination, object> ParameterMapAction;
            //public readonly Func<TSource, object, TDestination> ParameterMapFunc;
            // TODO 1つのクラスで済ますか
            // TODO クラスのメンバ定義の検討
            // TODO コンテキストの必要性チェック
            // TODO 中間構造の作成？

            throw new NotImplementedException();
        }
    }
}
