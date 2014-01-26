using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ICacheDao {
        
        // get one cache by its id
        Cache GetById(int id);
        
        // returns a list of all caches sizes
        List<string> GetAllCacheSizes();

        // rerturns a filtered subset of all caches
        List<Cache> GetCachesMatchingFilter(DataFilter filter);

        // for finding caches on web plattform
        List<Cache> GetByOwner(int userId);
             
        // base for statistical output 
        DateTime GetEarliestCacheCreationDate();
        DateTime GetLatestCacheCreationDate();

        GeoPosition GetLowestCachePosition();
        GeoPosition GetHighestCachePosition();

        List<StatisticData> GetHiddenCachesCountPerUser(DataFilter filter);
        List<StatisticData> GetBestRatedCaches(DataFilter filter);
        List<StatisticData> GetCacheDistributionBySize(DataFilter filter);
        List<StatisticData> GetCacheDistributionByCacheDifficulty(DataFilter filter);
        List<StatisticData> GetCacheDistributionByTerrainDifficulty(DataFilter filter);
        
        // write
        int Insert(Cache cache);
        bool Update(Cache cache);
        bool Delete(int cacheId);
    }
}