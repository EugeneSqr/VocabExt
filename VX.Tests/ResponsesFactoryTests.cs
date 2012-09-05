using NUnit.Framework;
using VX.Domain.Responses;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories.Responses;

namespace VX.Tests
{
    [TestFixture]
    public class ResponsesFactoryTests : TestsBase<IResponsesFactory, ResponsesFactory>
    {
        public ResponsesFactoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("ResponsesFactoryTests")]
        [Description("Creates new instance of ServiceOperationResponse with error message")]
        public void CreateErrorResponseTest()
        {
            var actual = SystemUnderTest.Create(false, ServiceOperationAction.None, "error");
            CheckResponse(false, null, "error", ServiceOperationAction.None, actual);
        }

        [Test]
        [Category("ResponsesFactoryTests")]
        [Description("Creates new instance of ServiceOperationResponse with status message")]
        public void CreateStatusResponseTest()
        {
            var actual = SystemUnderTest.Create(true, ServiceOperationAction.None, "status");
            CheckResponse(true, "status", null, ServiceOperationAction.None, actual);
        }

        private static void CheckResponse(
            bool expectedStatus,
            string expectedStatusMessage,
            string expectedErrorMessage,
            ServiceOperationAction expectedAction,
            IOperationResponse actual)
        {
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(expectedStatusMessage, actual.StatusMessage);
            Assert.AreEqual((int)expectedAction, actual.OperationActionCode);
            Assert.AreEqual(expectedErrorMessage, actual.ErrorMessage);
        }
    }
}
