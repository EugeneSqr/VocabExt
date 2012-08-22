namespace VX.Service.Infrastructure.Factories.Converters
{
    public abstract class ConverterFactory : IConverterFactory
    {
        public T Create<T>()
        {
            var factoryMethod = this as IConverterFactoryMethod<T>;
            return factoryMethod != null 
                ? factoryMethod.Create() 
                : default(T);
        }
    }
}