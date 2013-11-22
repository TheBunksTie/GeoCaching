using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.Common.DaoInterface {
    public interface ICacheDao {
        // read 
        Cache GetById(int id);
        IList<Cache> GetAll();

        // for finding caches on web plattform
        IList<Cache> GetByOwner(string userName);
        IList<Cache> GetByCacheDifficulty(double diffictulty, FilterCriterium criterium);
        IList<Cache> GetByTerrainDifficulty(double diffictulty, FilterCriterium criterium);
        IList<Cache> GetByAverageRating(double rating, FilterCriterium criterium);
        IList<Cache> GetBySize(CacheSize size, FilterCriterium criterium);

        // base for statistical output 
        IList<Cache> GetInRegionCreatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        IList<Cache> GetInRegionFoundBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);
        IList<Cache> GetInRegionRatedBetween(DateTime begin, DateTime end, GeoPosition from, GeoPosition to);

        // write
        int Insert(Cache cache);
        bool Update(Cache cache);
        bool Delete(int cacheId);
    }
}