using Swk5.GeoCaching.BusinessLogic.FilterManager;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.StatisticsManager {
    public class StatisticsManager : AbstractManagerBase, IStatisticsManager {
        private readonly ICacheDao cacheDao = DalFactory.CreateCacheDao(database);
        private readonly IFilterManager filterManager = GeoCachingBLFactory.GetFilterManager();
        private readonly ILogEntryDao logEntryDao = DalFactory.CreateLogEntryDao(database);

        public DataFilter GetDefaultFilter() {
            return filterManager.GetDefaultFilter();
        }

        // ------------------------- Top Lists ------------------------------------

        public StatisticDataset GetUsersByFoundCaches(DataFilter filter) {
            filterManager.ValidateFilter(filter);
            var data = new StatisticDataset {
                Data = logEntryDao.GetFoundCachesCountPerUser(filter),
                Caption = "Users ordered by number of found caches",
                Column2Caption = "Username",
                Column3Caption = "Found caches"
            };
            return data;
        }

        public StatisticDataset GetUsersByHiddenCaches(DataFilter filter) {
            filterManager.ValidateFilter(filter);
            var data = new StatisticDataset {
                Data = cacheDao.GetHiddenCachesCountPerUser(filter),
                Caption = "Users ordered by number of hidden caches",
                Column2Caption = "Username",
                Column3Caption = "Hidden caches"
            };
            return data;
        }

        public StatisticDataset GetCachesByRating(DataFilter filter) {
            filterManager.ValidateFilter(filter);
            var data = new StatisticDataset {
                Data = cacheDao.GetBestRatedCaches(filter),
                Caption = "Best rated caches",
                Column2Caption = "Cache name",
                Column3Caption = "Rating [1 - 10]"
            };
            return data;
        }

        public StatisticDataset GetCachesByLogCount(DataFilter filter) {
            filterManager.ValidateFilter(filter);
            var data = new StatisticDataset {
                Data = logEntryDao.GetMostLoggedCaches(filter),
                Caption = "Most logged caches",
                Column2Caption = "Cache name",
                Column3Caption = "Count of log entries"
            };
            return data;
        }

        // ------------------------- Distributions ------------------------------------

        public StatisticDataset GetCacheDistributionBySize(DataFilter filter) {
            filterManager.ValidateFilter(filter);
            var data = new StatisticDataset {
                Data = cacheDao.GetCacheDistributionBySize(filter),
                Caption = "Distribution of caches by size",
                Column2Caption = "Category of size",
                Column3Caption = "Share [%]"
            };
            return data;
        }

        public StatisticDataset GetCacheDistributionByCacheDifficulty(DataFilter filter) {
            filterManager.ValidateFilter(filter);
            var data = new StatisticDataset {
                Data = cacheDao.GetCacheDistributionByCacheDifficulty(filter),
                Caption = "Distribution of caches by cache difficulty",
                Column2Caption = "Difficulty Level",
                Column3Caption = "Share [%]"
            };
            return data;
        }

        public StatisticDataset GetCacheDistributionByTerrainDifficulty(DataFilter filter) {
            filterManager.ValidateFilter(filter);
            var data = new StatisticDataset {
                Data = cacheDao.GetCacheDistributionByTerrainDifficulty(filter),
                Caption = "Distribution of caches by terrain difficulty",
                Column2Caption = "Difficulty Level",
                Column3Caption = "Share [%]"
            };
            return data;
        }
    }
}