﻿using System.Transactions;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.DataContracts;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Tests;
using VX.Tests.Mocks;

namespace VX.IntegrationTests.RepositoriesTests
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

            ContainerBuilder.RegisterInstance(MockServiceOperationResponceFactory())
                .As<IServiceOperationResponseFactory>()
                .SingleInstance();

            ContainerBuilder.RegisterInstance(MockInputDataConverter())
                .As<IInputDataConverter>()
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
            mock.Setup(
                cacheFacade => cacheFacade.PutIntoCache(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
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
            mock.Setup(factory => factory.Build(It.IsAny<bool>(), It.IsAny<string>())).Returns(
                (bool status, string message) => new ServiceOperationResponse(status, message));

            return mock.Object;
        }

        private static IInputDataConverter MockInputDataConverter()
        {
            var mock = new Mock<IInputDataConverter>();
            mock.Setup(item => item.EmptyId)
                .Returns(-1);
            mock.Setup(item => item.Convert(It.IsAny<string>()))
                .Returns((string input) => int.Parse(input));
            return mock.Object;
        }
    }
}