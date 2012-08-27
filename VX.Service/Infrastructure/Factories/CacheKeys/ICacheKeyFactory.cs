namespace VX.Service.Infrastructure.Factories.CacheKeys
{
    public interface ICacheKeyFactory
    {
        string BuildKey(string serviceName, int[] parameters);

        string BuildKey(string serviceName, int parameter);

        string BuildKey(string serviceName, string parameter);

        string BuildKey(string serviceName, int[] parameters, bool flag);
    }
}