using NUnit.Framework;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.RepositoriesTests
{
    [TestFixture]
    internal class LanguagesRepositoryTests : RepositoryTestsBase<ILanguagesRepository, LanguagesRepository>
    {
        public LanguagesRepositoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("LanguageRepository")]
        [Description("Checks GetLanguage method returns correct value")]
        public void LanguageRepositoryGetLanguageTest()
        {
            Assert.AreNotEqual(SystemUnderTest.GetLanguage(1), null);
        }

        [Test]
        [Category("LanguageRepository")]
        [Description("Checks GetLanguage method returns empty value if language doesn't exist")]
        public void LanguageRepositoryGetLanguageEmptyTest()
        {
            Assert.AreEqual(SystemUnderTest.GetLanguage(-1), null);
        }
    }
}
