using System.Collections.Generic;
using System.IO;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public interface ICacheManager {

        // returns the currently authenticated user
        User GetAuthenticatedUser();
           
        // retrieve list of all existing caches
        //List<Cache> GetCacheList();
       
        // retrieve filtered subset of all caches
        List<Cache> GetFilteredCacheList(DataFilter filter);

        // returns a default filter for the whole cache dataset
        DataFilter GetDefaultFilter();

        // get a cache by its id
        Cache GetCacheById(int cacheId);

        // retrieve look up list of all define cache sizes
        List<string> GetCacheSizeList();

        // get owner of cache
        User GetCacheOwner(Cache c);

        // retrieve all assigned picture for one cache
        List<Image> GetImagesForCache(int cacheId);

        List<LogEntry> GetLogEntriesforCache(int cacheId);
        double GetAverageRatingForCache(int cacheId);

        // upload and associate a certain image with a cache
        Image UploadImage(int cacheId, Stream imageStream, string fileExtension);

        // creates a new default cache at the provided position, owner should authenticated user
        Cache CreateNewPositionedCache(int ownerId, double latitude, double longitude);
      
        // only updates exisiting cache
        bool UpdateExisitingCache(Cache c);

        // add a new entry to a certain caches log book 
        bool AddLogEntryForCache(LogEntry entry);

        // add a new rating for a certain cache
        bool AddRatingForCache(Rating rating);

        // deletion methods
        bool DeleteCache(int cacheId);
        bool DeleteImage(Image image);
    }
}