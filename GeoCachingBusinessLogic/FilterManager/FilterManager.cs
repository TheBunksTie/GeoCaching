using System;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.FilterManager {
    public class FilterManager : AbstractManagerBase, IFilterManager {
        private static readonly ICacheDao cacheDao = DalFactory.CreateCacheDao(database);

        public DataFilter GetDefaultFilter() {
            return new DataFilter {
                FromPosition = cacheDao.GetLowestCachePosition(),
                ToPosition = cacheDao.GetHighestCachePosition(),
                FromDate = cacheDao.GetEarliestCacheCreationDate(),
                ToDate = cacheDao.GetLatestCacheCreationDate(),
                FromCacheDifficulty = 1.0,
                ToCacheDifficulty = 5.0,
                FromTerrainDifficulty = 1.0,
                ToTerrainDifficulty = 5.0,
                FromCacheSize = 1,
                ToCacheSize = 6,
                CacheName = "",
                // limit only applies to top-X list of statistical data, therefore the typical default
                Limit = 10
            };
        }

        public void ValidateFilter(DataFilter filter) {
            if (filter == null) {
                throw new Exception("Error: The provided data filter is corrupt.");
            }

            if (filter.FromCacheDifficulty < 1 || filter.FromCacheDifficulty > 5) {
                throw new Exception("Error: The provided lower bound for cache difficulty is invalid.");
            }

            if (filter.ToCacheDifficulty < 1 || filter.ToCacheDifficulty > 5) {
                throw new Exception("Error: The provided upper bound for cache difficulty is invalid.");
            }

            if (filter.FromTerrainDifficulty < 1 || filter.FromTerrainDifficulty > 5) {
                throw new Exception("Error: The provided lower bound for terrain difficulty is invalid.");
            }

            if (filter.ToTerrainDifficulty < 1 || filter.ToTerrainDifficulty > 5) {
                throw new Exception("Error: The provided upper bound for terrain difficulty is invalid.");
            }

            if (filter.FromDate == null) {
                throw new Exception("Error: The provided lower bound for date is invalid.");
            }

            if (filter.ToDate == null) {
                throw new Exception("Error: The provided upper bound for date is invalid.");
            }

            if (filter.FromPosition.Latitude < 1 || filter.FromPosition.Longitude < 1) {
                throw new Exception("Error: The provided lower bound for position is invalid.");
            }

            if (filter.ToPosition.Latitude < 1 || filter.ToPosition.Longitude < 1) {
                throw new Exception("Error: The provided upper bound for position is invalid.");
            }

            if (filter.Limit < 1) {
                throw new Exception("Error: The provided top list limit is invalid.");
            }
        }
    }
}