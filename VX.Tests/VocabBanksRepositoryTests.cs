using Autofac;
using NUnit.Framework;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Factories;
using VX.Domain.Interfaces.Repositories;
using VX.Service;
using VX.Service.Repositories;
using VX.Tests.Mocks;

namespace VX.Tests
{
    [TestFixture]
    internal class VocabBanksRepositoryTests
    {
        private readonly IContainer container;
        
        public VocabBanksRepositoryTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<VocabBanksRepository>()
                .As<IVocabBanksRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ServiceSettingsMock>()
                .As<IServiceSettings>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntitiesFactoryMock>()
                .As<IEntitiesFactory>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if method returns vocabbanks list")]
        public void GetVocabBanksTest()
        {
            var repositoryUnderTest = container.Resolve<IVocabBanksRepository>();
            var actual = repositoryUnderTest.GetVocabBanks();
            Assert.AreEqual(actual.Count, 1);
        }
    }
}
