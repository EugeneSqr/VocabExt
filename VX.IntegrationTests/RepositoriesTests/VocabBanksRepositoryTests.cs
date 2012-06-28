using NUnit.Framework;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    internal class VocabBanksRepositoryTests : RepositoryTestsBase<IVocabBanksRepository, VocabBanksRepository>
    {
        public VocabBanksRepositoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if GetVocabBanks returns vocabbanks list")]
        public void GetVocabBanksPositiveTest()
        {
            Assert.AreEqual(7, SystemUnderTest.GetVocabBanks().Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if GetVocabBanksList returns list without translations and still with tags")]
        public void GetVocabBanksListPositiveTest()
        {
            var actual = SystemUnderTest.GetVocabBanksList();
            Assert.AreEqual(7, actual.Count);
            foreach (IVocabBank vocabBank in actual)
            {
                Assert.AreEqual(0, vocabBank.Translations.Count);
            }

            Assert.Greater(actual[0].Tags.Count, 0);
        }
    }
}
