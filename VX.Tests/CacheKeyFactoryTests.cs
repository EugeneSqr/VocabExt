using System;
using NUnit.Framework;
using VX.Service.Infrastructure.Factories.CacheKeys;

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
        [Description("Checks if BuildKey correctly builds a key from empty int array")]
        public void BuildKeyIntArrayEmptyTest()
        {
            Assert.AreEqual(
                "serviceName:", 
                SystemUnderTest.BuildKey("serviceName", new int[] { }));
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

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey correctly builds a key from int array and a bool flag")]
        public void BuildKeyIntArrayBoolTest()
        {
            Assert.AreEqual(
                "serviceName:1-2-3-flag:True", 
                SystemUnderTest.BuildKey("serviceName", new[] { 1, 2, 3 }, true));
        }

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey correctly builds a key feom empty int array and a bool flag")]
        public void BuildKeyEmptyIntArrayBoolTest()
        {
            Assert.AreEqual(
                "serviceName:flag:True", 
                SystemUnderTest.BuildKey("serviceName", new int[] { }, true));
        }

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey throws ArgumentNullException in input array is null")]
        public void BuildKeyNullIntArrayTest()
        {
            Assert.Throws<ArgumentNullException>(() => SystemUnderTest.BuildKey("serviceName", (int[]) null));
        }
    }
}
