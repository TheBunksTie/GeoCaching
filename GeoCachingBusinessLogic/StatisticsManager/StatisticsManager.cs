using System;
using System.Collections.Generic;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.StatisticsManager {
    public class StatisticsManager : AbstractManagerBase, IStatisticsManager {
        private readonly ICacheDao cacheDao = DalFactory.CreateCacheDao(database);
        private readonly ILogEntryDao logEntryDao = DalFactory.CreateLogEntryDao(database);
        private readonly IRatingDao ratingDao = DalFactory.CreateRatingDao(database);
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);

        public DateTime GetEarliestCacheCreationDate() {
            return cacheDao.GetEarliestCacheCreationDate();
        }

        public DateTime GetLatestCacheCreationDate() {
            return cacheDao.GetLatestCacheCreationDate();
        }

        public GeoPosition GetLowestCachePosition() {
            return cacheDao.GetLowestCachePosition();
        }

        public GeoPosition GetHighestCachePosition() {
            return cacheDao.GetHighestCachePosition();
        }

        public List<StatisticData> GetFoundCachesPerUser(Filter limitation) {
            return logEntryDao.GetFoundCachesCountPerUser(limitation.FromDate,
                limitation.ToDate,
                limitation.FromPosition,
                limitation.ToPosition);
        }

        public List<StatisticData> GetHiddenCachesPerUser(Filter limitation) {
            return cacheDao.GetHiddenCachesCountPerUser(limitation.FromDate,
                limitation.ToDate,
                limitation.FromPosition,
                limitation.ToPosition);
        }

        public List<StatisticData> GetCacheDistributionBySize(Filter limitation) {
            return cacheDao.GetCacheDistributionBySize(limitation.FromDate,
                  limitation.ToDate,
                  limitation.FromPosition,
                  limitation.ToPosition);
        }

        public List<StatisticData> GetCacheDistributionByCacheDifficulty(Filter limitation) {
            return cacheDao.GetCacheDistributionByCacheDifficulty(limitation.FromDate,
                  limitation.ToDate,
                  limitation.FromPosition,
                  limitation.ToPosition);
        }

        public List<StatisticData> GetCacheDistributionByTerrainDifficulty(Filter limitation) {
            return cacheDao.GetCacheDistributionByTerrainDifficulty(limitation.FromDate,
                  limitation.ToDate,
                  limitation.FromPosition,
                  limitation.ToPosition);
        }
    }
}