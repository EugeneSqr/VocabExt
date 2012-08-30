using VX.Domain.Surrogates;

namespace VX.Service.Infrastructure.Factories
{
    public interface IResponseFactoryMethod<T>
    {
        T Create(bool status, ServiceOperationAction action);

        T Create(bool status, ServiceOperationAction action, string message);
    }
}
