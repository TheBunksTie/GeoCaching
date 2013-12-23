using System.Collections.Generic;
using System.IO;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public interface ICacheManager {
        // retrieve list of all existing caches
        List<Cache> GetCacheList();

        // retrieve filtered subset of all caches
        List<Cache> GetFilteredCacheList(FilterCriterium criterium, FilterOperation operation, string filterValue);

        // retrieve look up list of all define cache sizes
        List<string> GetCacheSizeList();

        // get username for given id
        User GetCacheOwner(Cache c);

        // retrieve all assigned picture for one cache
        List<Image> GetImagesForCache(int cacheId);

        // upload and associate a certain image with a cache
        Image UploadImage (int cacheId, Stream imageStream, string fileExtension);

        // creates a new cache with default values
        bool CreateNewDefaultCache();

        Cache CreateNewPositionedCache(int ownerId, double latitude, double longitude);

        bool CreateNewCacheFromData(Cache c);

        // only updates exisiting cache
        bool UpdateExisitingCache(Cache c);

        // try to delete cache
        bool DeleteCache(int cacheId);
        void DeleteImage(Image image);
    }
}