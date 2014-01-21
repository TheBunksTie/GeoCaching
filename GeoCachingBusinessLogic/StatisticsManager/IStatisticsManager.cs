using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.StatisticsManager {
    public interface IStatisticsManager {
        DataFilter GetDefaultFilter();

        StatisticDataset GetFoundCachesByUser(DataFilter filter);
        StatisticDataset GetHiddenCachesByUser(DataFilter filter);

        StatisticDataset GetCacheDistributionBySize(DataFilter filter);
        StatisticDataset GetCacheDistributionByCacheDifficulty(DataFilter filter);
        StatisticDataset GetCacheDistributionByTerrainDifficulty(DataFilter filter);

        StatisticDataset GetBestRatedCaches(DataFilter filter);
        StatisticDataset GetMostLoggedCaches(DataFilter filter);
    }
}