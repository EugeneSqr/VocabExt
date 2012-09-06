using System.Linq;
using Autofac;
using NUnit.Framework;
using VX.IntegrationTests.Mocks;
using VX.Service.Infrastructure.Factories.Responses;
using VX.Service.Infrastructure.Factories.SearchStrings;
using VX.Service.Infrastructure.Factories.Surrogates;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators;
using VX.Service.Validators.Interfaces;
using VX.Tests.Mocks;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    internal class VocabBanksRepositoryTests : DataLayerTestsBase<IVocabBanksRepository, VocabBanksRepository>
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

            ContainerBuilder.RegisterType<SearchStringFactory>()
                .As<ISearchStringFactory>()
                .InstancePerLifetimeScope();

            ContainerBuilder.RegisterType<WordValidatorMock>()
                .As<IWordValidator>()
                .InstancePerLifetimeScope();

            ContainerBuilder.RegisterType<WordsRepository>()
                .As<IWordsRepository>()
                .InstancePerLifetimeScope();

            ContainerBuilder.RegisterType<ResponsesFactory>()
                .As<IResponsesFactory>()
                .InstancePerLifetimeScope();
            #endregion

            ContainerBuilder.RegisterType<SurrogatesFactoryMock>()
                .As<ISurrogatesFactory>()
                .InstancePerLifetimeScope();
            
            BuildContainer();
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if Get returns vocabbanks list")]
        public void GetVocabBanksPositiveTest()
        {
            Assert.AreEqual(7, SystemUnderTest.Get().Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if Get returns vocabbanks by specified ids")]
        public void GetVocabBanksByIds()
        {
            Assert.AreEqual(3, SystemUnderTest.Get(new[] {1, 2, 3}).Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if Get returns vocabbanks by specified ids with translations only")]
        public void GetVocabBanksByIdsWithTranslationsOnlyTest()
        {
            Assert.AreEqual(3, SystemUnderTest.GetWithTranslationsOnly(new[] {1, 2, 3}).Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if Get returns vocabbanks list with translations only")]
        public void GetVocabBanksWithTranslationsOnlyTest()
        {
            Assert.AreEqual(7, SystemUnderTest.GetWithTranslationsOnly().Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if GetSummary returns list without translations")]
        public void GetSummaryPositiveTest()
        {
            var actual = SystemUnderTest.GetSummary();
            Assert.AreEqual(7, actual.Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if attach translation attaches new translation")]
        public void AttachTranslationTest()
        {
            Assert.IsTrue(SystemUnderTest.AttachTranslation(2, 1));
            var repositoryForCheckingResultOfAttach = Container.Resolve<ITranslationsRepository>();
            Assert.IsNotNull(
                repositoryForCheckingResultOfAttach.GetTranslations(2).FirstOrDefault(item => item.Id == 1));
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if attach translation does not attach translation that already exist in a bank")]
        public void AttachExistTranslationTest()
        {
            Assert.IsFalse(SystemUnderTest.AttachTranslation(1, 1));
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if detach translation detaches translation")]
        public void DetachTranslationTest()
        {
            Assert.IsTrue(SystemUnderTest.DetachTranslation(1, 1));
            var repositoryForCheckingResultOfDetach = Container.Resolve<ITranslationsRepository>();
            Assert.IsNull(
                repositoryForCheckingResultOfDetach.GetTranslations(1).FirstOrDefault(item => item.Id == 1));
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Check if UpdateSummary updates Name and Description of the bank")]
        public void UpdateSummaryTest()
        {
            var bankForUpdate = SystemUnderTest.GetSummary()[0];
            bankForUpdate.Name = "new name";
            bankForUpdate.Description = "new description";
            SystemUnderTest.UpdateSummary(bankForUpdate);
            var actual = SystemUnderTest.Get()[0];
            Assert.AreEqual("new name", actual.Name);
            Assert.AreEqual("new description", actual.Description);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if Create creates new empty bank")]
        public void CreateTest()
        {
            var actual = SystemUnderTest.Create();
            Assert.IsNotNull(actual);
            Assert.Greater(actual.Id, 0);
            Assert.AreEqual(SystemUnderTest.NewVocabBankName, actual.Name);
            Assert.IsNull(actual.Description);
            Assert.IsNotNull(actual.Translations);
            Assert.AreEqual(0, actual.Translations.Count);
            Assert.IsNotNull(actual.Tags);
            Assert.AreEqual(0, actual.Tags.Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if Delete deletes specified vocabulary bank")]
        public void DeleteTest()
        {
            Assert.IsTrue(SystemUnderTest.Delete(1));
            Assert.AreEqual(0, SystemUnderTest.Get(new[] { 1 }).Count);
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if Delete fails to delete a bank if it doesn't exist")]
        public void DeleteFailTest()
        {
            Assert.IsFalse(SystemUnderTest.Delete(999));
        }
    }
}
