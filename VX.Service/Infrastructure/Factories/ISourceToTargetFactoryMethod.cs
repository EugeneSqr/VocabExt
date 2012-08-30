namespace VX.Service.Infrastructure.Factories
{
    public interface ISourceToTargetFactoryMethod<TTarget, TSource>
    {
        TTarget Create(TSource source);
    }
}