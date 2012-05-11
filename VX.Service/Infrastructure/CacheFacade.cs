using System;
using System.Web;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    public class CacheFacade : ICacheFacade
    {
        public void PutIntoCache(object item, string cacheKey)
        {
            HttpRuntime.Cache.Insert(
                cacheKey, 
                item, 
                null, 
                System.Web.Caching.Cache.NoAbsoluteExpiration, 
                TimeSpan.FromSeconds(300)); // TODO: value to config
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