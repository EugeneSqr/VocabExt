using Autofac;
using Moq;
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

            ContainerBuilder.RegisterInstance(MockCacheFacade().Object)
                .As<ICacheFacade>()
                .SingleInstance();

            ContainerBuilder.RegisterInstance(MockCacheKeyFactory().Object)
                .As<ICacheKeyFactory>()
                .SingleInstance();
            
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

        private Mock<ICacheFacade> MockCacheFacade()
        {
            var mock = new Mock<ICacheFacade>();
            string outvalue;
            mock.Setup(cacheFacade => cacheFacade.PutIntoCache(It.IsAny<object>(), It.IsAny<string>()));
            mock.Setup(cacheFacade => cacheFacade.GetFromCache(It.IsAny<string>(), out outvalue))
                .Returns(false);

            return mock;
        }

        private Mock<ICacheKeyFactory> MockCacheKeyFactory()
        {
            var mock = new Mock<ICacheKeyFactory>();
            mock.Setup(cacheKeyFactory => cacheKeyFactory.BuildKey(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("serviceName:string");
            mock.Setup(cacheKeyFactory => cacheKeyFactory.BuildKey(It.IsAny<string>(), It.IsAny<int[]>()))
                .Returns("serviceName:1-2-3-");
            return mock;
        }
    }
}
