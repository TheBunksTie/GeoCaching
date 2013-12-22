using System;
using System.Collections.Generic;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.StatisticsManager {
    public interface IStatisticsManager {
        DateTime GetEarliestCacheCreationDate();
        DateTime GetLatestCacheCreationDate();

        GeoPosition GetLowestCachePosition();
        GeoPosition GetHighestCachePosition();

        List<StatisticData> GetFoundCachesPerUser(Filter limitation);
        List<StatisticData> GetHiddenCachesPerUser(Filter limitation);

        List<StatisticData> GetCacheDistributionBySize(Filter limitation);
        List<StatisticData> GetCacheDistributionByCacheDifficulty(Filter limitation);
        List<StatisticData> GetCacheDistributionByTerrainDifficulty(Filter limitation);
    }
}