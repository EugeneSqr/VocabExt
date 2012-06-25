using NUnit.Framework;
using VX.Service.Factories;
using VX.Service.Factories.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    public class CacheKeyFactoryTests : TestsBase<ICacheKeyFactory, CacheKeyFactory>
    {
        public CacheKeyFactoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey concatenates service name and parameter")]
        public void BuildKeyStringTest()
        {
            Assert.AreEqual(
                "serviceName:stringParameter", 
                SystemUnderTest.BuildKey("serviceName", "stringParameter"));
        }

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey correctly builds a key from int array")]
        public void BuildKeyIntArrayTest()
        {
            Assert.AreEqual(
                "serviceName:1-2-3-", 
                SystemUnderTest.BuildKey("serviceName", new[] {1, 2, 3}));
        }

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey correctly builds a key from single int")]
        public void BuildKeyIntTest()
        {
            Assert.AreEqual(
                "serviceName:1",
                SystemUnderTest.BuildKey("serviceName", 1));
        }
    }
}
