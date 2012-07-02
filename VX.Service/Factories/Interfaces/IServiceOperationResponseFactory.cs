using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Factories.Interfaces
{
    public interface IServiceOperationResponseFactory
    {
        IServiceOperationResponse Build(bool status, string errorMessage);
    }
}
