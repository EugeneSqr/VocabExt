using System.Collections.Generic;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Responses;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.VocabExtServiceTests
{
    // TODO: finish unit tests
    [TestFixture]
    public class VocabExtServiceMainFixture : VocabExtServiceBaseFixture
    {
        public VocabExtServiceMainFixture()
        {
            ContainerBuilder.RegisterInstance(MockVocabBankRepository())
                .As<IVocabBanksRepository>().SingleInstance();
            
            BuildContainer();
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("GetTasks gets not empty tasks list")]
        public void GetTasksTest()
        {
            Assert.AreEqual(1, SystemUnderTest.GetTasks().Count); 
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("GetTasks with parameters Gets not empty tasks list")]
        public void GetTasksParatersTest()
        {
            Assert.AreEqual(1, SystemUnderTest.GetTasks(new []{1}).Count);
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("GetVocabBanksSummary returns summary")]
        public void GetVocabBanksSummaryTest()
        {
            var actual = SystemUnderTest.GetVocabBanksSummary();
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual("testName", actual[0].Name);
            Assert.AreEqual("testDescription", actual[0].Description);
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("GetTranslations returns translations")]
        public void GetTranslationsTest()
        {
            Assert.AreEqual(1, SystemUnderTest.GetTranslations("1").Count);
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("GetTranslations returns empty list if input id is incorrect")]
        public void GetTranslationsEmptyListTest()
        {
            Assert.AreEqual(0, SystemUnderTest.GetTranslations("").Count);
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("GetWords returns not empty list of words")]
        public void GetWordsTest()
        {
            Assert.AreEqual(1, SystemUnderTest.GetWords("test").Count);
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("GetLanguages returns not empty list of languages")]
        public void GetLanguagesTest()
        {
            Assert.AreEqual(1, SystemUnderTest.GetLanguages().Count);
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("DeleteVocabularyBank creates positive response")]
        public void DeleteVocabulatyBankTest()
        {
            var actual = SystemUnderTest.DeleteVocabularyBank("1");
            CheckResponse(true, ServiceOperationAction.Delete, actual);
        }

        [Test]
        [Category("VocabExtServiceTests.Main")]
        [Description("DeleteVocabularyBank creates negative response if input is incorrect")]
        public void DeleteVocabularyBankEmptyTest()
        {
            var actual = SystemUnderTest.DeleteVocabularyBank("asdf");
            CheckResponse(false, ServiceOperationAction.Delete, actual);
        }

        private static void CheckResponse(
            bool expectedStatus, 
            ServiceOperationAction expectedAction, 
            IOperationResponse actual)
        {
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual((int)expectedAction, actual.OperationActionCode);
        }

        private static IVocabBanksRepository MockVocabBankRepository()
        {
            var mock = new Mock<IVocabBanksRepository>();
            mock.Setup(repo => repo.GetWithTranslationsOnly())
                .Returns(new List<IVocabBank> {new VocabBankContract()} );

            mock.Setup(repo => repo.GetWithTranslationsOnly(It.IsAny<int[]>()))
                .Returns(new List<IVocabBank> {new VocabBankContract()});

            mock.Setup(repo => repo.GetSummary()).Returns(new List<IVocabBankSummary>
                                                              {
                                                                  new VocabBankSummary
                                                                      {
                                                                          Id = 1,
                                                                          Name = "testName",
                                                                          Description = "testDescription"
                                                                      }
                                                              });

            mock.Setup(repo => repo.Delete(0)).Returns(false);
            mock.Setup(repo => repo.Delete(It.IsInRange(0, int.MaxValue, Range.Exclusive)))
                .Returns(true);
            
            return mock.Object;
        }
    }
}
