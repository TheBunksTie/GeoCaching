using System;

namespace Swk5.GeoCaching.DomainModel {
    public enum StatisticalOperation {
        CachesFoundByUser,
        CachesHiddenByUser,
        CacheDistributionBySize,
        CacheDistributionByCacheDifficulty,
        CacheDistributionByTerrainDifficulty
    }

    public static class StatisticalOperationExtension {

        public static string UiCaption ( this StatisticalOperation operation ) {
            if ( operation == StatisticalOperation.CachesFoundByUser ) {
                return "Users ordered by number of found caches";
            }
            if ( operation == StatisticalOperation.CachesHiddenByUser ) {
                return "Users ordered by number of hidden caches";
            }

            if ( operation == StatisticalOperation.CacheDistributionBySize ) {
                return "Distribution of caches by size";
            }
            if ( operation == StatisticalOperation.CacheDistributionByCacheDifficulty ) {
                return "Distribution of caches by cache difficulty";
            }
            if ( operation == StatisticalOperation.CacheDistributionByTerrainDifficulty ) {
                return "Distribution of caches by terrain difficulty";
            }
            throw new ArgumentException();
        }
    }
}