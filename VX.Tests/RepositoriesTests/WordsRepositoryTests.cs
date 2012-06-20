using Autofac;
using Moq;
using NUnit.Framework;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.RepositoriesTests
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
            var actual = SystemUnderTest.GetWords(null);
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWords returns empty list if input is empty")]
        public void GetWordsEmptyInputTest()
        {
            var actual = SystemUnderTest.GetWords(string.Empty);
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        [Category("WordsRepositoryTests")]
        [Description("Checks if GetWords returns empty list if input is lesser than minimal")]
        public void GetWordsLesserThanMinimalTest()
        {
            var actual = SystemUnderTest.GetWords("t");
            Assert.AreEqual(0, actual.Count);
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
