namespace VX.Service.Infrastructure.Factories.Adapters
{
    public abstract class AdapterFactory : IAdapterFactory
    {
        public TTarget Create<TTarget, TAdaptee>(TAdaptee entity)
        {
            var factoryMethod = this as IAdapterFactoryMethod<TTarget, TAdaptee>;
            return factoryMethod != null 
                ? factoryMethod.Create(entity) 
                : default(TTarget);
        }
    }
}