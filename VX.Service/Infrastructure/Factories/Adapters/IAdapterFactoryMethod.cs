namespace VX.Service.Infrastructure.Factories.Adapters
{
    public interface IAdapterFactoryMethod<TTarget, TAdaptee>
    {
        TTarget Create(TAdaptee entity);
    }
}