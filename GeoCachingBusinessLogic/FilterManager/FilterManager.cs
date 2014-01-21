using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.FilterManager {
    public class FilterManager : AbstractManagerBase, IFilterManager {
        private static readonly ICacheDao cacheDao= DalFactory.CreateCacheDao(database);

        public DataFilter GetDefaultFilter() {
            return new DataFilter {
                FromPosition = cacheDao.GetLowestCachePosition(),
                ToPosition = cacheDao.GetHighestCachePosition(),
                FromCreationDate = cacheDao.GetEarliestCacheCreationDate(),
                ToCreationDate = cacheDao.GetLatestCacheCreationDate(),
                FromCacheDifficulty = 1.0,
                ToCacheDifficulty = 5.0,
                FromTerrainDifficulty = 1.0,
                ToTerrainDifficulty = 5.0,
                FromCacheSize = 1,
                ToCacheSize = 6,
                CacheName = ""
            };
        }
    }
}
