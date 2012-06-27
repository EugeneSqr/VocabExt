using System;
using System.Web;
using System.Web.Caching;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    public class CacheFacade : ICacheFacade
    {
        private readonly IServiceSettings serviceSettings;
        
        public CacheFacade(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public void PutIntoCache(object item, string cacheKey)
        {
            HttpRuntime.Cache.Insert(
                cacheKey, 
                item, 
                null, 
                Cache.NoAbsoluteExpiration, 
                TimeSpan.FromSeconds(serviceSettings.CacheSlidingExpirationSeconds));
        }

        public void PutIntoCache(object item, string cacheKey, string dependentTableName)
        {
            HttpRuntime.Cache.Insert(
                cacheKey,
                item,
                new SqlCacheDependency(serviceSettings.DomainDatabaseName, dependentTableName),
                Cache.NoAbsoluteExpiration,
                TimeSpan.FromSeconds(serviceSettings.CacheSlidingExpirationSeconds));
        }

        public bool GetFromCache<T>(string cacheKey, out T cachedItem)
        {
            cachedItem = default(T);
            try
            {
                var item = HttpRuntime.Cache.Get(cacheKey);
                if (item is T)
                {
                    cachedItem = (T)item;
                    return true;
                }
                
                return false;
                
            }
            catch (Exception)
            {
                // TODO: add logging
                return false;
            }
        }
    }
}