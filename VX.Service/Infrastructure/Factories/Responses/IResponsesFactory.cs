using VX.Domain.Responses;
using VX.Domain.Surrogates;

namespace VX.Service.Infrastructure.Factories.Responses
{
    public interface IResponsesFactory
    {
        IOperationResponse Create(bool status, ServiceOperationAction action);

        IOperationResponse Create(bool status, ServiceOperationAction action, string message);
    }
}