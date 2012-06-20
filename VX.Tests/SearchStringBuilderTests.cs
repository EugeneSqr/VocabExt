using NUnit.Framework;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class SearchStringBuilderTests : TestsBase<ISearchStringBuilder, SearchStringBuilder>
    {
        public SearchStringBuilderTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("SearchStringBuilderTests")]
        [Description("Checks if BuildSearchString returns actual search string")]
        public void BuildSearchStringTest()
        {
            Assert.AreEqual("te", SystemUnderTest.BuildSearchString("teststring"));
        }

        [Test]
        [Category("SearchStringBuilderTests")]
        [Description("Checks if BuildSearchString returns empty string if input is incorrect")]
        public void BuildSearchStringIncorrectInputTest()
        {
            Assert.AreEqual(string.Empty, SystemUnderTest.BuildSearchString(string.Empty));
        }

        [Test]
        [Category("SearchStringBuilderTests")]
        [Description("Checks if BuildSearchString returns empty string if inpull less than minimal")]
        public void BuildSearchStringLessThanMinimalTest()
        {
            Assert.AreEqual(string.Empty, SystemUnderTest.BuildSearchString("t"));
        }
    }
}
