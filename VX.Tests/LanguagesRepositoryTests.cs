using Autofac;
using NUnit.Framework;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;


namespace VX.Tests
{
    [TestFixture]
    internal class LanguagesRepositoryTests : RepositoryTestsBase
    {
        public LanguagesRepositoryTests()
        {
            ContainerBuilder.RegisterType<LanguagesRepository>()
                .As<ILanguagesRepository>()
                .InstancePerLifetimeScope();

            BuildContainer();
        }

        [Test]
        [Category("LanguageRepository")]
        [Description("Checks GetLanguage method returns correct value")]
        public void LanguageRepositoryGetLanguageTest()
        {
            const int testId = 1;
            var repositoryUnderTest = Container.Resolve<ILanguagesRepository>();
            Assert.AreNotEqual(repositoryUnderTest.GetLanguage(testId), null);
        }

        [Test]
        [Category("LanguageRepository")]
        [Description("Checks GetLanguage method returns empty value if language doesn't exist")]
        public void LanguageRepositoryGetLanguageEmptyTest()
        {
            var repositoyUnderTest = Container.Resolve<ILanguagesRepository>();
            Assert.AreEqual(repositoyUnderTest.GetLanguage(-1), null);
        }
    }
}
