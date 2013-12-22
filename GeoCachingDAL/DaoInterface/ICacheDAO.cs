using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ICacheDao {
        // read 
        Cache GetById(int id);
        List<Cache> GetAll();

        List<string> GetAllCacheSizes();

        // for finding caches on web plattform
        List<Cache> GetByOwner(int userId);

        List<Cache> GetCacheByCriterium(FilterCriterium criterium, FilterOperation operation, string value);

        //List<Cache> GetByCacheDifficulty(double diffictulty, FilterOperation operation);
        //List<Cache> GetByTerrainDifficulty(double diffictulty, FilterOperation operation);
        //List<Cache> GetByAverageRating(double rating, FilterOperation operation);
        //List<Cache> GetBySize(string size, FilterOperation operation);

        // base for statistical output 
        List<Cache> GetInRegionCreatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        List<Cache> GetInRegionFoundBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        List<Cache> GetInRegionRatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);

        // write
        int Insert(Cache cache);
        bool Update(Cache cache);
        bool Delete(int cacheId);
    }
}