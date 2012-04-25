using Autofac;
using NUnit.Framework;
using VX.Domain.Interfaces;
using VX.Service;
using VX.Service.Repositories;
using VX.Tests.Mocks;

namespace VX.Tests
{
    [TestFixture]
    internal class LanguagesRepositoryTests
    {
        private readonly IContainer container;
        
        public LanguagesRepositoryTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LanguagesRepository>()
                .As<ILanguagesRepository>()
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
        [Category("LanguageRepository")]
        [Description("Checks GetLanguage method returns correct value")]
        public void LanguageRepositoryGetLanguageTest()
        {
            const int testId = 1;
            var repositoryUnderTest = container.Resolve<ILanguagesRepository>();
            Assert.AreNotEqual(repositoryUnderTest.GetLanguage(testId), null);
        }

        [Test]
        [Category("LanguageRepository")]
        [Description("Checks GetLanguage method returns empty value if language doesn't exist")]
        public void LanguageRepositoryGetLanguageEmptyTest()
        {
            var repositoyUnderTest = container.Resolve<ILanguagesRepository>();
            Assert.AreEqual(repositoyUnderTest.GetLanguage(-1), null);
        }
    }
}
