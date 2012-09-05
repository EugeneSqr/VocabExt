namespace VX.Service.Infrastructure.Factories.Entities
{
    public interface IAbstractEntitiesFactory
    {
        TTarget Create<TTarget, TSource>(TSource source);

        T Create<T>();
    }
}