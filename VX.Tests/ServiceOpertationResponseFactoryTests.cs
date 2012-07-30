using NUnit.Framework;
using VX.Service.Factories;
using VX.Service.Factories.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class ServiceOpertationResponseFactoryTests : TestsBase<IServiceOperationResponseFactory, ServiceOperationResponseFactory>
    {
        public ServiceOpertationResponseFactoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("ServiceOperationResponseFactoryTests")]
        [Description("Checks if build creates new instance of ServiceOperationResponse with error message")]
        public void BuildErrorResponseTest()
        {
            var actual = SystemUnderTest.Build(false, "error");
            Assert.IsFalse(actual.Status);
            Assert.AreEqual("error", actual.ErrorMessage);
            Assert.IsNullOrEmpty(actual.StatusMessage);
        }

        [Test]
        [Category("ServiceOperationResponseFactoryTests")]
        [Description("Checks if build creates new instance of ServiceOperationResponse with status message")]
        public void BuildStatusResponseTest()
        {
            var actual = SystemUnderTest.Build(true, "status");
            Assert.IsTrue(actual.Status);
            Assert.AreEqual("status", actual.StatusMessage);
            Assert.IsNullOrEmpty(actual.ErrorMessage);
        }
    }
}
