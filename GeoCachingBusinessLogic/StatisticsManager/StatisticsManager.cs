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

        public List<StatisticData> GetFoundCachesPerUser(CacheFilter limitation) {
            return logEntryDao.GetFoundCachesCountPerUser(limitation.FromCreationDate,
                limitation.ToCreationDate,
                limitation.FromPosition,
                limitation.ToPosition);
        }

        public List<StatisticData> GetHiddenCachesPerUser(CacheFilter limitation) {
            return cacheDao.GetHiddenCachesCountPerUser(limitation.FromCreationDate,
                limitation.ToCreationDate,
                limitation.FromPosition,
                limitation.ToPosition);
        }

        public List<StatisticData> GetCacheDistributionBySize(CacheFilter limitation) {
            return cacheDao.GetCacheDistributionBySize(limitation.FromCreationDate,
                limitation.ToCreationDate,
                limitation.FromPosition,
                limitation.ToPosition);
        }

        public List<StatisticData> GetCacheDistributionByCacheDifficulty(CacheFilter limitation) {
            return cacheDao.GetCacheDistributionByCacheDifficulty(limitation.FromCreationDate,
                limitation.ToCreationDate,
                limitation.FromPosition,
                limitation.ToPosition);
        }

        public List<StatisticData> GetCacheDistributionByTerrainDifficulty(CacheFilter limitation) {
            return cacheDao.GetCacheDistributionByTerrainDifficulty(limitation.FromCreationDate,
                limitation.ToCreationDate,
                limitation.FromPosition,
                limitation.ToPosition);
        }
    }
}