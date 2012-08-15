using System.Transactions;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain;
using VX.IntegrationTests.Mocks;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Tests;
using VX.Tests.Mocks;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    public abstract class DataLayerTestsBase<TType, TImplementation> : TestsBase<TType, TImplementation>
    {
        private TransactionScope scope;
        
        protected DataLayerTestsBase()
        {
            ContainerBuilder.RegisterInstance(MockCacheFacade())
                .As<ICacheFacade>()
                .SingleInstance();

            ContainerBuilder.RegisterInstance(MockCacheKeyFactory())
                .As<ICacheKeyFactory>()
                .SingleInstance();

            ContainerBuilder.RegisterInstance(MockServiceOperationResponceFactory())
                .As<IServiceOperationResponseFactory>()
                .SingleInstance();

            ContainerBuilder.RegisterType<ContextFactoryMock>()
                .As<IContextFactory>()
                .SingleInstance();

            ContainerBuilder.RegisterType<EntitiesFactoryMock>()
                .As<IEntitiesFactory>()
                .InstancePerLifetimeScope();
        }

        [SetUp]
        protected void SetUp()
        {
            scope = new TransactionScope();
        }

        [TearDown]
        protected void TearDown()
        {
            scope.Dispose();
        }

        private static ICacheFacade MockCacheFacade()
        {
            var mock = new Mock<ICacheFacade>();
            string outvalue;
            mock.Setup(cacheFacade => cacheFacade.PutIntoCache(It.IsAny<object>(), It.IsAny<string>()));
            mock.Setup(
                cacheFacade => cacheFacade.PutIntoCache(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()));
            mock.Setup(cacheFacade => cacheFacade.GetFromCache(It.IsAny<string>(), out outvalue))
                .Returns(false);

            return mock.Object;
        }

        private static ICacheKeyFactory MockCacheKeyFactory()
        {
            var mock = new Mock<ICacheKeyFactory>();
            mock.Setup(cacheKeyFactory => cacheKeyFactory.BuildKey(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("serviceName:string");
            mock.Setup(cacheKeyFactory => cacheKeyFactory.BuildKey(It.IsAny<string>(), It.IsAny<int[]>()))
                .Returns("serviceName:1-2-3-");
            return mock.Object;
        }

        private static IServiceOperationResponseFactory MockServiceOperationResponceFactory()
        {
            var mock = new Mock<IServiceOperationResponseFactory>();
            mock.Setup(factory => factory.Build(It.IsAny<bool>(), It.IsAny<ServiceOperationAction>(), It.IsAny<string>()))
                .Returns((bool status, ServiceOperationAction action, string message) => new ServiceOperationResponse(status, action) { StatusMessage = message });

            mock.Setup(factory => factory.Build(It.IsAny<bool>(), It.IsAny<ServiceOperationAction>()))
                .Returns((bool status, ServiceOperationAction action) => new ServiceOperationResponse(status, action));

            return mock.Object;
        }
    }
}
