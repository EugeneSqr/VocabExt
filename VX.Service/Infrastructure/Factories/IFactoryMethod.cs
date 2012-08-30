namespace VX.Service.Infrastructure.Factories
{
    public interface IFactoryMethod<T>
    {
        T Create();
    }
}