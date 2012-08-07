using NUnit.Framework;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    internal class LanguagesRepositoryTests : RepositoryTestsBase<ILanguagesRepository, LanguagesRepository>
    {
        public LanguagesRepositoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("LanguageRepositoryTests")]
        [Description("Checks if GetLanguage method returns correct value")]
        public void GetLanguageTest()
        {
            Assert.IsNotNull(SystemUnderTest.GetLanguage(1));
        }

        [Test]
        [Category("LanguageRepositoryTests")]
        [Description("Checks if GetLanguage method returns empty value if language doesn't exist")]
        public void GetLanguageEmptyTest()
        {
            Assert.IsNull(SystemUnderTest.GetLanguage(-1), null);
        }

        [Test]
        [Category("LanguageRepositoryTests")]
        [Description("Checks if GetLanguages method returns all languages")]
        public void GetLanguagesTest()
        {
            Assert.AreEqual(2, SystemUnderTest.GetLanguages().Count);
        }
    }
}
