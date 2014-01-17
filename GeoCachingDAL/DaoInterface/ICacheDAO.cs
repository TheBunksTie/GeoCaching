using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ICacheDao {
        // read 
        Cache GetById(int id);
        List<Cache> GetAll();

        List<string> GetAllCacheSizes();

        List<Cache> GetCachesMatchingFilter(CacheFilter filter);

        // for finding caches on web plattform
        List<Cache> GetByOwner(int userId);

        List<Cache> GetCachesByCriterium(FilterCriterium criterium, FilterOperation operation, string value);
        List<Cache> GetByAverageRating(double rating, FilterOperation operation);

        // base for statistical output 
        DateTime GetEarliestCacheCreationDate();
        DateTime GetLatestCacheCreationDate();

        GeoPosition GetLowestCachePosition();
        GeoPosition GetHighestCachePosition();

        List<StatisticData> GetHiddenCachesCountPerUser(DateTime fromDate,
            DateTime toDate,
            GeoPosition fromPosition,
            GeoPosition toPosition);

        List<StatisticData> GetCacheDistributionBySize(DateTime fromDate,
            DateTime toDate,
            GeoPosition fromPosition,
            GeoPosition toPosition);

        List<StatisticData> GetCacheDistributionByCacheDifficulty(DateTime fromDate,
            DateTime toDate,
            GeoPosition fromPosition,
            GeoPosition toPosition);

        List<StatisticData> GetCacheDistributionByTerrainDifficulty(DateTime fromDate,
            DateTime toDate,
            GeoPosition fromPosition,
            GeoPosition toPosition);

        List<Cache> GetInRegionCreatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        List<Cache> GetInRegionFoundBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        List<Cache> GetInRegionRatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);

        // write
        int Insert(Cache cache);
        bool Update(Cache cache);
        bool Delete(int cacheId);
    }
}