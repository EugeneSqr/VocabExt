using NUnit.Framework;
using VX.Service.Infrastructure.Factories.SearchStrings;

namespace VX.Tests.FactoriesTests
{
    [TestFixture]
    internal class SearchStringFactoryTests : TestsBase<ISearchStringFactory, SearchStringFactory>
    {
        public SearchStringFactoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("SearchStringBuilderTests")]
        [Description("Checks if Create returns actual search string")]
        public void BuildSearchStringTest()
        {
            Assert.AreEqual("te", SystemUnderTest.Create("teststring"));
        }

        [Test]
        [Category("SearchStringBuilderTests")]
        [Description("Checks if Create returns empty string if input is incorrect")]
        public void BuildSearchStringIncorrectInputTest()
        {
            Assert.AreEqual(string.Empty, SystemUnderTest.Create(string.Empty));
        }

        [Test]
        [Category("SearchStringBuilderTests")]
        [Description("Checks if Create returns empty string if inpull less than minimal")]
        public void BuildSearchStringLessThanMinimalTest()
        {
            Assert.AreEqual(string.Empty, SystemUnderTest.Create("t"));
        }
    }
}
