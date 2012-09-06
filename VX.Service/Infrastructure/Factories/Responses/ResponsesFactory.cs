using VX.Domain.Responses;
using VX.Domain.Responses.Impl;
using VX.Domain.Surrogates;

namespace VX.Service.Infrastructure.Factories.Responses
{
    public class ResponsesFactory : IResponsesFactory
    {
        public IOperationResponse Create(bool status, ServiceOperationAction action)
        {
            return new ServiceOperationResponse(status, action);
        }

        public IOperationResponse Create(bool status, ServiceOperationAction action, string message)
        {
            var result = Create(status, action);
            if (status)
            {
                result.StatusMessage = message;
            }
            else
            {
                result.ErrorMessage = message;
            }

            return result;
        }
    }
}