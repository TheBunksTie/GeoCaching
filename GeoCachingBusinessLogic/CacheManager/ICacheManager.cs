using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public interface ICacheManager {
        // retrieve list of all existing caches
        List<Cache> GetCacheList();

        // retrieve look up list of all define cache sizes
        List<string> GetCacheSizeList();

        // creates a new cache with default values
        bool CreateNewDefaultCache(Cache c);

        // only updates exisiting cache
        bool UpdateExisitingCache(Cache c);

        // try to delete cache
        bool DeleteCache(Cache c);
    }
}