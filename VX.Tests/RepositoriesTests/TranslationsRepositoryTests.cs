using NUnit.Framework;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.RepositoriesTests
{
    [TestFixture]
    internal class TranslationsRepositoryTest : RepositoryTestsBase<ITranslationsRepository, TranslationsRepository>
    {
        public TranslationsRepositoryTest()
        {
            BuildContainer();
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns list of translations if parameters are correct")]
        public void GetTranslationsPositiveTest()
        {
            var actual = SystemUnderTest.GetTranslations("1");
            Assert.Greater(actual.Count, 0);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns empty list if id is incorrect")]
        public void GetTranslationsNegativeBadIdTest()
        {
            var actual = SystemUnderTest.GetTranslations("asd");
            Assert.AreEqual(0, actual.Count);
        }
    }
}
