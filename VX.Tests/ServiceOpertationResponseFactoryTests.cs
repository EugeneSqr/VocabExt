using NUnit.Framework;
using VX.Domain;
using VX.Service.Infrastructure.Factories.ServiceOperationResponses;

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
            var actual = SystemUnderTest.Build(false, ServiceOperationAction.None, "error");
            Assert.IsFalse(actual.Status);
            Assert.AreEqual("error", actual.ErrorMessage);
            Assert.AreEqual(0, actual.OperationActionCode);
            Assert.IsNullOrEmpty(actual.StatusMessage);
        }

        [Test]
        [Category("ServiceOperationResponseFactoryTests")]
        [Description("Checks if build creates new instance of ServiceOperationResponse with status message")]
        public void BuildStatusResponseTest()
        {
            var actual = SystemUnderTest.Build(true, ServiceOperationAction.None, "status");
            Assert.IsTrue(actual.Status);
            Assert.AreEqual("status", actual.StatusMessage);
            Assert.AreEqual(0, actual.OperationActionCode);
            Assert.IsNullOrEmpty(actual.ErrorMessage);
        }
    }
}
