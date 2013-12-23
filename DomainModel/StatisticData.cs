using System;

namespace Swk5.GeoCaching.DomainModel {
    public class StatisticData : IEquatable<StatisticData> {
        public string Nr { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public bool Equals(StatisticData other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return string.Equals(Name, other.Name) && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals(( StatisticData ) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }

    public enum StatisticalOperation {
        CachesFoundByUser,
        CachesHiddenByUser,
        CacheDistributionBySize,
        CacheDistributionByCacheDifficulty,
        CacheDistributionByTerrainDifficulty
    }

    public static class StatisticalOperationExtension {
        public static string UiCaption(this StatisticalOperation operation) {
            if (operation == StatisticalOperation.CachesFoundByUser) {
                return "Users ordered by number of found caches";
            }
            if (operation == StatisticalOperation.CachesHiddenByUser) {
                return "Users ordered by number of hidden caches";
            }

            if (operation == StatisticalOperation.CacheDistributionBySize) {
                return "Distribution of caches by size";
            }
            if (operation == StatisticalOperation.CacheDistributionByCacheDifficulty) {
                return "Distribution of caches by cache difficulty";
            }
            if (operation == StatisticalOperation.CacheDistributionByTerrainDifficulty) {
                return "Distribution of caches by terrain difficulty";
            }
            throw new ArgumentException();
        }
    }
}