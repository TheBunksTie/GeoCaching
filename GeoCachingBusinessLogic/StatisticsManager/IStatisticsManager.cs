using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.StatisticsManager {
    public interface IStatisticsManager {
        DataFilter GetDefaultFilter();

        StatisticDataset GetUsersByFoundCaches(DataFilter filter);
        StatisticDataset GetUsersByHiddenCaches(DataFilter filter);

        StatisticDataset GetCachesByRating(DataFilter filter);
        StatisticDataset GetCachesByLogCount(DataFilter filter);

        StatisticDataset GetCacheDistributionBySize(DataFilter filter);
        StatisticDataset GetCacheDistributionByCacheDifficulty(DataFilter filter);
        StatisticDataset GetCacheDistributionByTerrainDifficulty(DataFilter filter);
    }
}