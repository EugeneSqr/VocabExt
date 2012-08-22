namespace VX.Service.Infrastructure.Factories.Converters
{
    public interface IConverterFactory
    {
        T Create<T>();
    }
}