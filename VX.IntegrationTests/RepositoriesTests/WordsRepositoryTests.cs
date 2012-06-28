using Autofac;
using Moq;
using NUnit.Framework;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    public class WordsRepositoryTests : RepositoryTestsBase<IWordsRepository, WordsRepository>
    {
        public WordsRepositoryTests()
        {
            ContainerBuilder.RegisterInstance(MockSearchStringBuilder())
                .As<ISearchStringBuilder>()
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

        private static ISearchStringBuilder MockSearchStringBuilder()
        {
            var mock = new Mock<ISearchStringBuilder>();
            mock.Setup(item => item.BuildSearchString(It.IsAny<string>()))
                .Returns((string item) => item == null || item.Length < 2 ? string.Empty : item);
            return mock.Object;
        }
    }
}
