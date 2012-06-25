namespace VX.Service.Factories.Interfaces
{
    public interface ICacheKeyFactory
    {
        string BuildKey(string serviceName, int[] parameters);

        string BuildKey(string serviceName, int parameter);

        string BuildKey(string serviceName, string parameter);
    }
}