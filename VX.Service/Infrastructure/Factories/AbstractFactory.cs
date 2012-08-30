using VX.Domain.Surrogates;

namespace VX.Service.Infrastructure.Factories
{
    public abstract class AbstractFactory : IAbstractFactory
    {
        public TTarget Create<TTarget, TSource>(TSource source)
        {
            var factoryMethod = this as ISourceToTargetFactoryMethod<TTarget, TSource>;
            return factoryMethod != null 
                ? factoryMethod.Create(source) 
                : default(TTarget);
        }

        public T Create<T>()
        {
            var factoryMethod = this as IFactoryMethod<T>;
            return factoryMethod != null
                ? factoryMethod.Create()
                : default(T);
        }

        public T Create<T>(bool status, ServiceOperationAction action)
        {
            var factoryMethod = this as IResponseFactoryMethod<T>;
            return factoryMethod != null
                       ? factoryMethod.Create(status, action)
                       : default(T);
        }

        public T Create<T>(bool status, ServiceOperationAction action, string message)
        {
            var factoryMethod = this as IResponseFactoryMethod<T>;
            return factoryMethod != null
                       ? factoryMethod.Create(status, action, message)
                       : default(T);
        }
    }
}