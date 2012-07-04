namespace VX.Service.Infrastructure.Interfaces
{
    public interface ICacheFacade
    {
        void PutIntoCache(object item, string cacheKey);

        void PutIntoCache(object item, string cacheKey, string[] dependencyTables);

        bool GetFromCache<T>(string cacheKey, out T cachedItem);
    }
}