using System.Collections.Generic;
using System.IO;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Infrastructure.Factories.Surrogates;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.VocabExtServiceTests
{
    [TestFixture(Description = "Repositories are empty")]
    public class EmptyRepositoriesFixture : VocabExtServiceBaseFixture
    {
        private readonly Stream falseSaveTranslationStream = new MemoryStream();
        private readonly Stream emptyTranslationSaveTranslationStream = new MemoryStream();
        private readonly Stream createSaveTranslationStream = new MemoryStream();
        private IManyToManyRelationship manyToMany;
        private IManyToManyRelationship emptyManyToMany;
        private IManyToManyRelationship createManyToMany = new ManyToManyRelationship();
        private ServiceOperationAction action = ServiceOperationAction.Create;
        private readonly ITranslation falseSaveTranaltionTranslation = new TranslationContract {Id = -1};
        private readonly ITranslation emptyTranslationSaveTranslationTranslation;
        private readonly ITranslation createSaveTranslationTranslation = new TranslationContract{Id = 1};
        
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
        [Description("SaveTranslation returns false if input is incorrect")]
        public void SaveTranslationFalseTest()
        {
            var actual = SystemUnderTest.SaveTranslation(falseSaveTranslationStream);
            CheckResponse(false, ServiceOperationAction.Create, actual);
        }

        [Test]
        [Category("VocabExtServiceTests.EmptyRepos")]
        [Description("SaveTranslation returns empty translation id input is incorrect")]
        public void SaveTranslationEmptyTest()
        {
            var actual = SystemUnderTest.SaveTranslation(emptyTranslationSaveTranslationStream);
            CheckResponse(false, ServiceOperationAction.Create, actual);
        }

        [Test]
        [Category("VocabExtServiceTests.EmptyRepos")]
        [Description("SaveTranslation returns setted translation on success")]
        public void SaveTranslationTest()
        {
            var actual = SystemUnderTest.SaveTranslation(createSaveTranslationStream);
            CheckResponse(true, ServiceOperationAction.Attach, actual);
        }

        private static IVocabBanksRepository MockVocabBankRepository()
        {
            var mock = new Mock<IVocabBanksRepository>();
            mock.Setup(repo => repo.Get()).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.GetWithTranslationsOnly()).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.GetWithTranslationsOnly(It.IsAny<int[]>())).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.AttachTranslation(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            return mock.Object;
        }

        private ITranslationsRepository MockTranslationsRepository()
        {
            var mock = new Mock<ITranslationsRepository>();
            mock.Setup(repo => repo.SaveTranslation(falseSaveTranaltionTranslation, out manyToMany, out action))
                .Returns(false);

            mock.Setup(repo => repo.SaveTranslation(emptyTranslationSaveTranslationTranslation, out emptyManyToMany, out action))
                .Returns(true);

            mock.Setup(repo => repo.SaveTranslation(createSaveTranslationTranslation, out createManyToMany, out action))
                .Returns(true);
            return mock.Object;
        }

        private ISurrogatesFactory MockSurrogatesFactory()
        {
            var mock = new Mock<ISurrogatesFactory>();
            mock.Setup(factory => factory.CreateBankTranslationPair(falseSaveTranslationStream))
                .Returns(new BankTranslationPair(-1, falseSaveTranaltionTranslation));
            mock.Setup(factory => factory.CreateBankTranslationPair(emptyTranslationSaveTranslationStream))
                .Returns(new BankTranslationPair(0, emptyTranslationSaveTranslationTranslation));
            mock.Setup(factory => factory.CreateBankTranslationPair(createSaveTranslationStream))
                .Returns(new BankTranslationPair(1, createSaveTranslationTranslation));
            return mock.Object;
        }
    }
}
