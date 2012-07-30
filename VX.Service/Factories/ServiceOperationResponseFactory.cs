using VX.Domain;
using VX.Service.Factories.Interfaces;

namespace VX.Service.Factories
{
    public class ServiceOperationResponseFactory : IServiceOperationResponseFactory
    {
        public IServiceOperationResponse Build(bool status, string message)
        {
            var result = new ServiceOperationResponse(status);
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