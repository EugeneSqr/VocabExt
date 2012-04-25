using Autofac;
using VX.Domain.Interfaces;
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

            builder.RegisterType<EntitiesFactory>()
                .As<IEntitiesFactory>()
                .InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}