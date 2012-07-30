using VX.Domain;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;

namespace VX.Service.Factories
{
    public class ServiceOperationResponceFactory : IServiceOperationResponseFactory
    {
        public IServiceOperationResponse Build(bool status, string errorMessage)
        {
            return status
                ? new ServiceOperationResponse(true, string.Empty)
                : new ServiceOperationResponse(false, errorMessage);
        }
    }
}