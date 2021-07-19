namespace WorkMapper.Mappers
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    using WorkMapper.Components;

    internal class EmitMapperFactory : IMapperFactory
    {
        private readonly IFactoryResolver factoryResolver;

        private readonly IConverterResolver converterResolver;

        private readonly IServiceProvider serviceProvider;

        private int typeNo;

        private AssemblyBuilder? assemblyBuilder;

        private ModuleBuilder? moduleBuilder;

        private ModuleBuilder ModuleBuilder
        {
            get
            {
                if (moduleBuilder is null)
                {
                    assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                        new AssemblyName("SmartMapperAssembly"),
                        AssemblyBuilderAccess.Run);
                    moduleBuilder = assemblyBuilder.DefineDynamicModule(
                        "SmartMapperModule");
                }

                return moduleBuilder;
            }
        }

        public EmitMapperFactory(
            IFactoryResolver factoryResolver,
            IConverterResolver converterResolver,
            IServiceProvider serviceProvider)
        {
            this.factoryResolver = factoryResolver;
            this.converterResolver = converterResolver;
            this.serviceProvider = serviceProvider;
        }

        // IConverterResolver, IFactoryResolver, IFunctionActivatorはDI

        public object Create(MapperCreateContext context)
        {
            // Mapper
            var mapper = CreateMapperInfo(context);

            // Holder
            var holder = CreateHolder(context);

            // TODO set member

            return mapper;
        }

        //--------------------------------------------------------------------------------
        // Mapper
        //--------------------------------------------------------------------------------

        private object CreateMapperInfo(MapperCreateContext context)
        {
            var type = typeof(MapperInfo<,>).MakeGenericType(context.MappingOption.SourceType, context.MappingOption.DestinationType);
            return Activator.CreateInstance(type)!;
        }

        //--------------------------------------------------------------------------------
        // Holder
        //--------------------------------------------------------------------------------

        private object CreateHolder(MapperCreateContext context)
        {
            var typeBuilder = ModuleBuilder.DefineType(
                $"Holder_{typeNo}",
                TypeAttributes.Public | TypeAttributes.AutoLayout | TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit);
            typeNo++;

            // TODO Define field

            var typeInfo = typeBuilder.CreateTypeInfo()!;
            var holderType = typeInfo.AsType();
            var holder = Activator.CreateInstance(holderType)!;

            // TODO Set field

            return holder;
        }

        //--------------------------------------------------------------------------------
        // Method
        //--------------------------------------------------------------------------------

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
    }
}
