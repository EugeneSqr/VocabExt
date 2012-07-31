using System.Linq;
using Autofac;
using NUnit.Framework;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.CompositeValidators;
using VX.Service.CompositeValidators.Interfaces;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    internal class VocabBanksRepositoryTests : RepositoryTestsBase<IVocabBanksRepository, VocabBanksRepository>
    {
        public VocabBanksRepositoryTests()
        {
            #region For checking attach/detach with another repo
            ContainerBuilder.RegisterType<TranslationsRepository>()
                .As<ITranslationsRepository>()
                .InstancePerLifetimeScope();

            ContainerBuilder.RegisterType<TranslationValidator>()
                .As<ITranslationValidator>()
                .InstancePerLifetimeScope();

            ContainerBuilder.RegisterType<SearchStringBuilder>()
                .As<ISearchStringBuilder>()
                .InstancePerLifetimeScope();

            ContainerBuilder.RegisterType<WordsRepository>()
                .As<IWordsRepository>()
                .InstancePerLifetimeScope();
            #endregion

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

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if attach translation attaches new translation")]
        public void AttachTranslationTest()
        {
            Assert.AreEqual(
                (int)ServiceOperationAction.Attach, 
                SystemUnderTest.AttachTranslation(2, 1).OperationActionCode);
            var repositoryForCheckingResultOfAttach = Container.Resolve<ITranslationsRepository>();
            Assert.IsNotNull(
                repositoryForCheckingResultOfAttach.GetTranslations(2).FirstOrDefault(item => item.Id == 1));
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if attach translation does not attach translation that already exist in a bank")]
        public void AttachExistantTranslationTest()
        {
            Assert.AreEqual(
                (int)ServiceOperationAction.None, 
                SystemUnderTest.AttachTranslation(1, 1).OperationActionCode);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if detach translation detaches translation")]
        public void DetachTranslationTest()
        {
            Assert.AreEqual(
                (int)ServiceOperationAction.Detach, 
                SystemUnderTest.DetachTranslation(1, 1).OperationActionCode);
            var repositoryForCheckingResultOfDetach = Container.Resolve<ITranslationsRepository>();
            Assert.IsNull(
                repositoryForCheckingResultOfDetach.GetTranslations(1).FirstOrDefault(item => item.Id == 1));

        }
    }
}
