using System;

namespace Swk5.GeoCaching.DomainModel {
    public class DataFilter {

        public DataFilter(DataFilter other) {
            CacheName = other.CacheName;
            FromPosition = new GeoPosition(other.FromPosition.Latitude, other.FromPosition.Longitude);
            ToPosition = new GeoPosition(other.ToPosition.Latitude, other.ToPosition.Longitude);
            
            FromDate = new DateTime(other.FromDate.Ticks);
            ToDate = new DateTime(other.ToDate.Ticks);

            FromCacheSize = other.FromCacheSize;
            ToCacheSize = other.ToCacheSize;

            FromCacheDifficulty = other.FromCacheDifficulty;
            ToCacheDifficulty = other.ToCacheDifficulty;

            FromTerrainDifficulty = other.FromTerrainDifficulty;
            ToTerrainDifficulty = other.ToTerrainDifficulty;

            Limit = other.Limit;

        }

        public DataFilter() {
            // empty constructor for component-wise initialization needed           
        }

        public String CacheName { get; set; }
        
        public GeoPosition FromPosition { get; set; }
        public GeoPosition ToPosition { get; set; }
        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int FromCacheSize { get; set; }
        public int ToCacheSize { get; set; }

        public double FromCacheDifficulty { get; set; }
        public double ToCacheDifficulty { get; set; }

        public double FromTerrainDifficulty { get; set; }
        public double ToTerrainDifficulty { get; set; }

        public int Limit { get; set; }        
    }
}