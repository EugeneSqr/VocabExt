using VX.Domain;

namespace VX.Service.Infrastructure.Factories.ServiceOperationResponses
{
    public class ServiceOperationResponseFactory : IServiceOperationResponseFactory
    {
        public IServiceOperationResponse Build(bool status, ServiceOperationAction action)
        {
            return new ServiceOperationResponse(status, action);
        }

        public IServiceOperationResponse Build(bool status, ServiceOperationAction action, string message)
        {
            var result = Build(status, action);
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