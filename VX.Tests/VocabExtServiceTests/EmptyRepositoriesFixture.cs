using System.Collections.Generic;
using System.IO;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Infrastructure.Exceptions;
using VX.Service.Infrastructure.Factories.Surrogates;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.VocabExtServiceTests
{
    [TestFixture(Description = "Repositories are empty")]
    public class EmptyRepositoriesFixture : VocabExtServiceBaseFixture
    {
        private readonly Stream incorrectTranslationStream = new MemoryStream();
        private readonly Stream incorrectSavedTranslationStream = new MemoryStream();
        private readonly Stream notAttachedTranalstionStream = new MemoryStream();
        private ServiceOperationAction action = ServiceOperationAction.Create;
        private readonly ITranslation incorrectTranslation = new TranslationContract {Id = -1};
        private readonly ITranslation incorrectSavedTranslation = new TranslationContract {Id = 1};
        private readonly ITranslation notAttachedTranslation = new TranslationContract{Id = 2};
        
        public EmptyRepositoriesFixture()
        {
            ContainerBuilder.RegisterInstance(MockVocabBankRepository())
                .As<IVocabBanksRepository>().SingleInstance();

            ContainerBuilder.RegisterInstance(MockTranslationsRepository())
                .As<ITranslationsRepository>().SingleInstance();

            ContainerBuilder.RegisterInstance(MockSurrogatesFactory())
                .As<ISurrogatesFactory>().SingleInstance();
            
            BuildContainer();
        }

        [Test]
        [Category("VocabExtServiceTests")]
        [Description("Gets task correctly")]
        public void GetTaskTest()
        {
            Assert.IsNotNull(SystemUnderTest.GetTask());
        }
        
        [Test]
        [Category("VocabExtServiceTests.EmptyRepos")]
        [Description("GetTasks without parameters returns empty list")]
        public void GetTasksEmptyListTest()
        {
            Assert.AreEqual(0, SystemUnderTest.GetTasks().Count); 
        }

        [Test]
        [Category("VocabExtServiceTests.EmptyRepos")]
        [Description("GetTasks with parameters returns empty list")]
        public void GetTasksParametersEmptyListTest()
        {
            Assert.AreEqual(0, SystemUnderTest.GetTasks(new[] {1}).Count);
        }

        [Test]
        [Category("VocabExtServiceTests.EmptyRepos")]
        [Description("SaveTranslation throws ValidaionFailedException if input translation is incorrect")]
        public void SaveTranslationValidationFailedTest()
        {
            var actual = SystemUnderTest.SaveTranslation(incorrectTranslationStream);
            CheckResponse(false, ServiceOperationAction.Create, actual);
        }

        [Test]
        [Category("VocabExtServiceTests.EmptyRepos")]
        [Description("SaveTranslation returns false response if input is incorrect")]
        public void SaveTranslationFalseTest()
        {
            var actual = SystemUnderTest.SaveTranslation(incorrectSavedTranslationStream);
            CheckResponse(false, ServiceOperationAction.Create, actual);
        }

        [Test]
        [Category("VocabExtServiceTests.EmptyRepos")]
        [Description("SaveTranslation saves and attaches translation")]
        public void SaveTranslationSaveAndAttachTest()
        {
            var actual = SystemUnderTest.SaveTranslation(notAttachedTranalstionStream);
            CheckResponse(true, ServiceOperationAction.Attach, actual);
        }

        private static IVocabBanksRepository MockVocabBankRepository()
        {
            var mock = new Mock<IVocabBanksRepository>();
            mock.Setup(repo => repo.Get()).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.GetWithTranslationsOnly()).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.GetWithTranslationsOnly(It.IsAny<int[]>())).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.AttachTranslation(It.IsAny<int>(), It.IsAny<int>()));
            return mock.Object;
        }

        private ITranslationsRepository MockTranslationsRepository()
        {
            var mock = new Mock<ITranslationsRepository>();
            mock.Setup(repo => repo.SaveTranslation(incorrectTranslation, out action))
                .Throws<ValidationFailedException>();

            mock.Setup(repo => repo.SaveTranslation(incorrectSavedTranslation, out action))
                .Returns(new ManyToManyRelationship());

            mock.Setup(repo => repo.SaveTranslation(notAttachedTranslation, out action)).Returns(
                new ManyToManyRelationship{Id = 1});
            
            return mock.Object;
        }

        private ISurrogatesFactory MockSurrogatesFactory()
        {
            var mock = new Mock<ISurrogatesFactory>();
            mock.Setup(factory => factory.CreateBankTranslationPair(incorrectTranslationStream))
                .Returns(new BankTranslationPair(-1, incorrectTranslation));
            mock.Setup(factory => factory.CreateBankTranslationPair(incorrectSavedTranslationStream))
                .Returns(new BankTranslationPair(0, incorrectSavedTranslation));
            mock.Setup(factory => factory.CreateBankTranslationPair(notAttachedTranalstionStream))
                .Returns(new BankTranslationPair(0, notAttachedTranslation));
            return mock.Object;
        }
    }
}
