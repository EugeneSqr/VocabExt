using Autofac;
using NUnit.Framework;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class VocabBanksRepositoryTests : RepositoryTestsBase
    {
        public VocabBanksRepositoryTests()
        {
            ContainerBuilder.RegisterType<VocabBanksRepository>()
                .As<IVocabBanksRepository>()
                .InstancePerLifetimeScope();

            BuildContainer();
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if GetVocabBanks returns vocabbanks list")]
        public void GetVocabBanksPositiveTest()
        {
            var repositoryUnderTest = Container.Resolve<IVocabBanksRepository>();
            var actual = repositoryUnderTest.GetVocabBanks();
            Assert.AreEqual(4, actual.Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if GetVocabBanksList returns list without translations and still with tags")]
        public void GetVocabBanksListPositiveTest()
        {
            var actual = Container.Resolve<IVocabBanksRepository>().GetVocabBanksList();
            Assert.AreEqual(4, actual.Count);
            foreach (IVocabBank vocabBank in actual)
            {
                Assert.AreEqual(0, vocabBank.Translations.Count);
            }

            Assert.Greater(actual[0].Tags.Count, 0);
        }
    }
}
