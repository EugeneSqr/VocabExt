using System.Collections.Generic;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.VocabExtServiceTests
{
    [TestFixture(Description = "Repositories are empty")]
    public class EmptyRepositoriesFixture : VocabExtServiceBaseFixture
    {
        public EmptyRepositoriesFixture()
        {
            ContainerBuilder.RegisterInstance(MockVocabBankRepository())
                .As<IVocabBanksRepository>().SingleInstance();
            
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

        private static IVocabBanksRepository MockVocabBankRepository()
        {
            var mock = new Mock<IVocabBanksRepository>();
            mock.Setup(repo => repo.Get()).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.GetWithTranslationsOnly()).Returns(new List<IVocabBank>());
            mock.Setup(repo => repo.GetWithTranslationsOnly(It.IsAny<int[]>())).Returns(new List<IVocabBank>());
            return mock.Object;
        }
    }
}
