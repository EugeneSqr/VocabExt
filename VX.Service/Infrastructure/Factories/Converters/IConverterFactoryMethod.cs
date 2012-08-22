namespace VX.Service.Infrastructure.Factories.Converters
{
    public interface IConverterFactoryMethod<T>
    {
        T Create();
    }
}