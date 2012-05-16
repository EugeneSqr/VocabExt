using Autofac;
using NUnit.Framework;
using VX.Service.Factories;
using VX.Service.Factories.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    public class CacheKeyFactoryTests
    {
        private readonly IContainer container;

        public CacheKeyFactoryTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CacheKeyFactory>()
                .As<ICacheKeyFactory>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey concatenates service name and parameter")]
        public void BuildKeyStringPositiveTest()
        {
            string actual = container.Resolve<ICacheKeyFactory>().BuildKey("serviceName", "stringParameter");
            Assert.AreEqual("serviceName:stringParameter", actual);
        }

        [Test]
        [Category("CacheKeyFactoryTests")]
        [Description("Checks if BuildKey correctly builds key from int array")]
        public void BuildKeyIntArrayPositiveTest()
        {
            string actual = container.Resolve<ICacheKeyFactory>().BuildKey("serviceName", new[] {1, 2, 3});
            Assert.AreEqual("serviceName:1-2-3-", actual);
        }
    }
}
