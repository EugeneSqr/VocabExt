namespace VX.Service.Infrastructure.Interfaces
{
    public interface ICacheFacade
    {
        void PutIntoCache(object item, string cacheKey);

        bool GetFromCache<T>(string cacheKey, out T cachedItem);
    }
}