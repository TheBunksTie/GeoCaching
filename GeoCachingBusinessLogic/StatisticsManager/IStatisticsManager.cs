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

        List<StatisticData> GetFoundCachesPerUser(CacheFilter limitation);
        List<StatisticData> GetHiddenCachesPerUser(CacheFilter limitation);

        List<StatisticData> GetCacheDistributionBySize(CacheFilter limitation);
        List<StatisticData> GetCacheDistributionByCacheDifficulty(CacheFilter limitation);
        List<StatisticData> GetCacheDistributionByTerrainDifficulty(CacheFilter limitation);
    }
}