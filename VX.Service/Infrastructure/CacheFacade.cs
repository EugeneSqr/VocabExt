using System;
using System.Web;
using System.Web.Caching;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    public class CacheFacade : ICacheFacade
    {
        private const int SingleElementArraySize = 1;
        private readonly IServiceSettings serviceSettings;
        
        public CacheFacade(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public void PutIntoCache(object item, string cacheKey)
        {
            PutIntoCache(item, cacheKey, (CacheDependency)null);
        }

        public void PutIntoCache(object item, string cacheKey, string[] dependencyTables)
        {
            if (dependencyTables.Length == SingleElementArraySize)
            {
                PutIntoCache(item, cacheKey, dependencyTables[0]);
            }
            else
            {
                var dependency = new AggregateCacheDependency();
                foreach (string dependencyTable in dependencyTables)
                {
                    dependency.Add(new SqlCacheDependency(serviceSettings.DomainDatabaseName, dependencyTable));
                }

                PutIntoCache(item, cacheKey, dependency);
            }
        }

        public bool GetFromCache<T>(string cacheKey, out T cachedItem)
        {
            cachedItem = default(T);
            try
            {
                var item = HttpRuntime.Cache.Get(cacheKey);
                if (item != CacheNullItem.Value && item is T)
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

        private void PutIntoCache(object item, string cacheKey, CacheDependency dependencies)
        {
            HttpRuntime.Cache.Insert(
                cacheKey,
                item ?? CacheNullItem.Value,
                dependencies,
                Cache.NoAbsoluteExpiration,
                TimeSpan.FromSeconds(serviceSettings.CacheSlidingExpirationSeconds));
        }

        private void PutIntoCache(object item, string cacheKey, string dependentTableName)
        {
            PutIntoCache(item, cacheKey, new SqlCacheDependency(serviceSettings.DomainDatabaseName, dependentTableName));
        }
    }
}