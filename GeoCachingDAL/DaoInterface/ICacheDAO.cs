using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ICacheDao {
        // read 
        Cache GetById(int id);
        List<Cache> GetAll();

        List<string> GetAllCacheSizes();

        List<Cache> GetCachesMatchingFilter(DataFilter filter);

        // for finding caches on web plattform
        List<Cache> GetByOwner(int userId);

        List<Cache> GetCachesByCriterium(FilterCriterium criterium, FilterOperation operation, string value);
        List<Cache> GetByAverageRating(double rating, FilterOperation operation);

        // base for statistical output 
        DateTime GetEarliestCacheCreationDate();
        DateTime GetLatestCacheCreationDate();

        GeoPosition GetLowestCachePosition();
        GeoPosition GetHighestCachePosition();

        List<StatisticData> GetHiddenCachesCountPerUser(DataFilter filter);
        List<StatisticData> GetCacheDistributionBySize(DataFilter filter);
        List<StatisticData> GetCacheDistributionByCacheDifficulty(DataFilter filter);
        List<StatisticData> GetCacheDistributionByTerrainDifficulty(DataFilter filter);
        List<StatisticData> GetBestRatedCaches(DataFilter filter);

        List<Cache> GetInRegionCreatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        List<Cache> GetInRegionFoundBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        List<Cache> GetInRegionRatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);

        // write
        int Insert(Cache cache);
        bool Update(Cache cache);
        bool Delete(int cacheId);
    }
}