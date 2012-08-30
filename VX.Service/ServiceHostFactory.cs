using System;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Factories;
using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.Context;
using VX.Service.Infrastructure.Factories.Tasks;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators;
using VX.Service.Validators.Interfaces;

namespace VX.Service
{
    public class ServiceHostFactory : AutofacHostFactory
    {
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<VocabExtService>();


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

            builder.RegisterType<Factory>()
                .As<IAbstractFactory>()
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

            builder.RegisterType<ContextFactory>()
                .As<IContextFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WordValidator>()
                .As<IWordValidator>()
                .InstancePerLifetimeScope();

            Container = builder.Build();

            return base.CreateServiceHost(constructorString, baseAddresses);
        }

        protected override ServiceHost CreateSingletonServiceHost(object singletonInstance, Uri[] baseAddresses)
        {
            if (singletonInstance == null)
            {
                throw new ArgumentNullException("singletonInstance");
            }
            if (baseAddresses == null)
            {
                throw new ArgumentNullException("baseAddresses");
            }
            return new ServiceHost(singletonInstance, baseAddresses);
        }
    }
}