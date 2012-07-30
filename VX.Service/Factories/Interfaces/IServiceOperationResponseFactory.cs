using VX.Domain;

namespace VX.Service.Factories.Interfaces
{
    public interface IServiceOperationResponseFactory
    {
        IServiceOperationResponse Build(bool status, string message);
    }
}
