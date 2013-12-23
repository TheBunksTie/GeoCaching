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

        // get owner of cache
        User GetCacheOwner(Cache c);

        // retrieve all assigned picture for one cache
        List<Image> GetImagesForCache(int cacheId);

        // upload and associate a certain image with a cache
        Image UploadImage (int cacheId, Stream imageStream, string fileExtension);


        Cache CreateNewPositionedCache(int ownerId, double latitude, double longitude);

        // only updates exisiting cache
        bool UpdateExisitingCache(Cache c);

        // deletion methods
        bool DeleteCache(int cacheId);
        bool DeleteImage(Image image);
    }
}