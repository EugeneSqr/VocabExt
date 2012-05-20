using Autofac;
using NUnit.Framework;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class TranslationsRepositoryTest : RepositoryTestsBase
    {
        public TranslationsRepositoryTest()
        {
            ContainerBuilder.RegisterType<TranslationsRepository>()
                .As<ITranslationsRepository>()
                .InstancePerLifetimeScope();

            BuildContainer();
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns list of translations if parameters are correct")]
        public void GetTranslationsPositiveTest()
        {
            var repositoryUnderTest = Container.Resolve<ITranslationsRepository>();
            var actual = repositoryUnderTest.GetTranslations("1");
            Assert.Greater(actual.Count, 0);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns empty list if id is incorrect")]
        public void GetTranslationsNegativeBadIdTest()
        {
            var repositoryUnderTest = Container.Resolve<ITranslationsRepository>();
            var actual = repositoryUnderTest.GetTranslations("asd");
            Assert.AreEqual(0, actual.Count);
        }
    }
}
