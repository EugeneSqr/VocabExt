using Autofac;
using VX.Domain.Interfaces.Factories;
using VX.Domain.Interfaces.Repositories;
using VX.Service.Factories;
using VX.Service.Interfaces;
using VX.Service.Repositories;

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

            builder.RegisterType<RandomFacade>()
                .As<IRandomFacade>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionPicker>()
                .As<IQuestionPicker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntitiesFactory>()
                .As<IEntitiesFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TasksFactory>()
                .As<ITasksFactory>()
                .InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}