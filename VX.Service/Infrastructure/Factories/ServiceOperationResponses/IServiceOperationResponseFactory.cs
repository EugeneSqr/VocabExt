using VX.Domain;

namespace VX.Service.Infrastructure.Factories.ServiceOperationResponses
{
    public interface IServiceOperationResponseFactory
    {
        IServiceOperationResponse Build(bool status, ServiceOperationAction action);

        IServiceOperationResponse Build(bool status, ServiceOperationAction action, string message);
    }
}
