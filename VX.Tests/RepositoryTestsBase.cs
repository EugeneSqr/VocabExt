using Autofac;
using VX.Service;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Tests.Mocks;

namespace VX.Tests
{
    internal abstract class RepositoryTestsBase
    {
        protected IContainer Container { get; set; }

        protected ContainerBuilder ContainerBuilder { get; set; }

        protected RepositoryTestsBase()
        {
            ContainerBuilder = new ContainerBuilder();

            ContainerBuilder.RegisterType<ServiceSettingsMock>()
                .As<IServiceSettings>()
                .InstancePerLifetimeScope();

            ContainerBuilder.RegisterType<EntitiesFactoryMock>()
                .As<IEntitiesFactory>()
                .InstancePerLifetimeScope();
        }

        protected void BuildContainer()
        {
            Container = ContainerBuilder.Build();
        }
    }
}
