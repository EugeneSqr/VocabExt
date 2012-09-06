using VX.Domain.Responses;
using VX.Domain.Responses.Impl;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories.Responses;

namespace VX.Tests.Mocks
{
    public class ResponsesFactoryMock : IResponsesFactory
    {
        public IOperationResponse Create(bool status, ServiceOperationAction action)
        {
            return new ServiceOperationResponse(status, action);
        }

        public IOperationResponse Create(bool status, ServiceOperationAction action, string message)
        {
            return status 
                    ? new ServiceOperationResponse(true, action) {StatusMessage = message} 
                    : new ServiceOperationResponse(false, action) {ErrorMessage = message};
        }
    }
}
