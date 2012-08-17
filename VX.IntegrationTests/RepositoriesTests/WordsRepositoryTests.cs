using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    public class WordsRepositoryTests : DataLayerTestsBase<IWordsRepository, WordsRepository>
    {
        private readonly IWord validationFailWord = new WordContract();

        private readonly IWord validationPassWord = new WordContract
                                                        {
                                                            Spelling = "this_is_test_spelling",
                                                            Transcription = "this_is_test_transcription",
                                                            Language = new LanguageContract
                                                                           {
                                                                               Id = 1
                                                                           }
                                                        };
        
        public WordsRepositoryTests()
        {
            ContainerBuilder.RegisterInstance(MockSearchStringBuilder())
                .As<ISearchStringBuilder>()
                .SingleInstance();
            ContainerBuilder.RegisterInstance(MockWordValidator())
                .As<IWordValidator>()
                .SingleInstance();

            BuildContainer();
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWords returns words by search string")]
        public void GetWordsRetrieveWordsTest()
        {
            var actual = SystemUnderTest.GetWords("ar");
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("arrogant", actual[0].Spelling);
            Assert.AreEqual("arrival", actual[1].Spelling);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWords is not case sensitive")]
        public void GetWordsCaseInsensitiveTest()
        {
            var actual = SystemUnderTest.GetWords("aR");
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("arrogant", actual[0].Spelling);
            Assert.AreEqual("arrival", actual[1].Spelling);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWords returns empty list if input is null")]
        public void GetWordsNullInputTest()
        {
            Assert.AreEqual(0, SystemUnderTest.GetWords(null).Count);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWords returns empty list if input is empty")]
        public void GetWordsEmptyInputTest()
        {
            Assert.AreEqual(0, SystemUnderTest.GetWords(string.Empty).Count);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWords returns empty list if input is lesser than minimal")]
        public void GetWordsLesserThanMinimalTest()
        {
            Assert.AreEqual(0, SystemUnderTest.GetWords("t").Count);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWord returns word if it is exists in storage")]
        public void GetWordTest()
        {
            Assert.AreEqual(1, SystemUnderTest.GetWord(1).Id);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWord returns null if word is not present in storage")]
        public void GetWordNullTest()
        {
            Assert.IsNull(SystemUnderTest.GetWord(-1));
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if SaveWord creates new word and returns status true")]
        public void SaveWordNewTest()
        {
            var actual = SystemUnderTest.SaveWord(validationPassWord);
            Assert.IsTrue(actual.Status);
            Assert.AreEqual((int)ServiceOperationAction.Create, actual.OperationActionCode);
            Assert.Greater(int.Parse(actual.StatusMessage), 0);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if SaveWord returns status false if validation fails")]
        public void SaveWordExistsTest()
        {
            var actual = SystemUnderTest.SaveWord(validationFailWord);
            Assert.IsFalse(actual.Status);
            Assert.AreEqual((int)ServiceOperationAction.Create, actual.OperationActionCode);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if CheckWordExists returns true if word exists")]
        public void CheckWordExistsTest()
        {
            var actual = SystemUnderTest.CheckWordExists("arrogant");
            Assert.IsTrue(actual);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if CheckWordExists returns false if word doesn't exist")]
        public void CheckWordExistsNullTest()
        {
            var actual = SystemUnderTest.CheckWordExists("asdljhfajsdhfk");
            Assert.IsFalse(actual);
        }

        private static ISearchStringBuilder MockSearchStringBuilder()
        {
            var mock = new Mock<ISearchStringBuilder>();
            mock.Setup(item => item.BuildSearchString(It.IsAny<string>()))
                .Returns((string item) => item == null || item.Length < 2 ? string.Empty : item);
            return mock.Object;
        }

        private IWordValidator MockWordValidator()
        {
            var mock = new Mock<IWordValidator>();
            mock.Setup(item => item.Validate(validationFailWord, It.IsAny<IWordsRepository>()))
                .Returns(new ServiceOperationResponse(false, ServiceOperationAction.Create));

            mock.Setup(item => item.Validate(validationPassWord, It.IsAny<IWordsRepository>()))
                .Returns(new ServiceOperationResponse(true, ServiceOperationAction.Create));

            return mock.Object;
        }
    }
}
