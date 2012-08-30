using VX.Domain.Surrogates;

namespace VX.Service.Infrastructure.Factories
{
    public interface IAbstractFactory
    {
        TTarget Create<TTarget, TSource>(TSource source);

        T Create<T>();

        T Create<T>(bool status, ServiceOperationAction action);

        T Create<T>(bool status, ServiceOperationAction action, string message);
    }
}