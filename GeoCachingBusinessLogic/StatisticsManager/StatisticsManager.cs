using System.Collections.Generic;
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

        public StatisticDataset GetFoundCachesByUser ( DataFilter filter ) {
            var data = new StatisticDataset {
                Data = logEntryDao.GetFoundCachesCountPerUser(filter),
                Caption = "Users ordered by number of found caches",
                ColumnCaption2 ="Username", 
                ColumnCaption3 = "Found caches"              
            };
            return data;
        }

        public StatisticDataset GetHiddenCachesByUser ( DataFilter filter ) {
            var data = new StatisticDataset {
                Data = cacheDao.GetHiddenCachesCountPerUser(filter),
                Caption = "Users ordered by number of hidden caches",
                ColumnCaption2 ="Username", 
                ColumnCaption3 = "Hidden caches"   
            };
            return data;            
        }

        public StatisticDataset GetCacheDistributionBySize ( DataFilter filter ) {
            var data = new StatisticDataset {
                Data = cacheDao.GetCacheDistributionBySize(filter),
                Caption = "Distribution of caches by size",
                ColumnCaption2 = "Category of size", 
                ColumnCaption3 = "Share"   
            };
            return data;
        }

        public StatisticDataset GetCacheDistributionByCacheDifficulty ( DataFilter filter ) {
            var data = new StatisticDataset {
                Data = cacheDao.GetCacheDistributionByCacheDifficulty(filter),
                Caption = "Distribution of caches by cache difficulty",
                ColumnCaption2 = "Difficulty Level", 
                ColumnCaption3 = "Share"                   
            };
            return data;
        }

        public StatisticDataset GetCacheDistributionByTerrainDifficulty ( DataFilter filter ) {
            var data = new StatisticDataset {
                Data = cacheDao.GetCacheDistributionByTerrainDifficulty(filter),
                Caption = "Distribution of caches by terrain difficulty",
                ColumnCaption2 = "Difficulty Level", 
                ColumnCaption3 = "Share"
            };
            return data;
        }

        public StatisticDataset GetBestRatedCaches ( DataFilter filter ) {
            var data = new StatisticDataset {
                Data = cacheDao.GetBestRatedCaches(filter),
                Caption = "Best rated caches",
                ColumnCaption2 ="Cache name", 
                ColumnCaption3 = "Rating"               
            };
            return data;
        }

        public StatisticDataset GetMostLoggedCaches ( DataFilter filter ) {
            var data = new StatisticDataset {
                Data = logEntryDao.GetMostLoggedCaches(filter),
                Caption = "Most logged caches",
                ColumnCaption2 ="Cache name", 
                ColumnCaption3 = "Count of log entries"                
            };
            return data;
        }
    }
}