namespace VX.Service.Infrastructure.Factories.Adapters
{
    public interface IAdapterFactory
    {
        TTarget Create<TTarget, TAdaptee>(TAdaptee entity);
    }
}