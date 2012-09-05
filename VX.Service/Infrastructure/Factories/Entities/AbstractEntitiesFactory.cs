namespace VX.Service.Infrastructure.Factories.Entities
{
    public abstract class AbstractEntitiesFactory : IAbstractEntitiesFactory
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
            var factoryMethod = this as IDefaultFactoryMethod<T>;
            return factoryMethod != null
                ? factoryMethod.Create()
                : default(T);
        }
    }
}