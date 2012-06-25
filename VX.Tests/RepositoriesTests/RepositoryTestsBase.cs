using System.Transactions;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Tests.Mocks;

namespace VX.Tests.RepositoriesTests
{
    [TestFixture]
    public abstract class RepositoryTestsBase<TType, TImplementation> : TestsBase<TType, TImplementation>
    {
        private TransactionScope scope;
        
        protected RepositoryTestsBase()
        {
            ContainerBuilder.RegisterInstance(MockCacheFacade())
                .As<ICacheFacade>()
                .SingleInstance();

            ContainerBuilder.RegisterInstance(MockCacheKeyFactory())
                .As<ICacheKeyFactory>()
                .SingleInstance();
            
            ContainerBuilder.RegisterType<ServiceSettingsMock>()
                .As<IServiceSettings>()
                .InstancePerLifetimeScope();

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
    }
}
