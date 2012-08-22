using Autofac;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Factories.Adapters;
using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.Converters;
using VX.Service.Infrastructure.Factories.EntitiesContext;
using VX.Service.Infrastructure.Factories.ServiceOperationResponses;
using VX.Service.Infrastructure.Factories.Tasks;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators;
using VX.Service.Validators.Interfaces;

namespace VX.Service
{
    internal static class Initializer
    {
        internal static IContainer Container { get; private set; }

        static Initializer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ServiceSettings>()
                .As<IServiceSettings>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LanguagesRepository>()
                .As<ILanguagesRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WordsRepository>()
                .As<IWordsRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VocabBanksRepository>()
                .As<IVocabBanksRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TranslationsRepository>()
                .As<ITranslationsRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RandomFacade>()
                .As<IRandomFacade>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RandomPicker>()
                .As<IRandomPicker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityAdapterFactory>()
                .As<IAdapterFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TaskValidator>()
                .As<ITaskValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TasksFactory>()
                .As<ITasksFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SynonymSelector>()
                .As<ISynonymSelector>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CacheFacade>()
                .As<ICacheFacade>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CacheKeyFactory>()
                .As<ICacheKeyFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SearchStringBuilder>()
                .As<ISearchStringBuilder>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TranslationValidator>()
                .As<ITranslationValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ServiceOperationResponseFactory>()
                .As<IServiceOperationResponseFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InputDataConverter>()
                .As<IInputDataConverter>()
                .InstancePerLifetimeScope();

            builder.RegisterType<JavaScroptConvertersFactory>()
                .As<IConverterFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ContextFactory>()
                .As<IContextFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WordValidator>()
                .As<IWordValidator>()
                .InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}