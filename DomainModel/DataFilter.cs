using System;

namespace Swk5.GeoCaching.DomainModel {
    public class DataFilter {

        public String CacheName { get; set; }
        
        public GeoPosition FromPosition { get; set; }
        public GeoPosition ToPosition { get; set; }
        
        public DateTime FromCreationDate { get; set; }
        public DateTime ToCreationDate { get; set; }

        public int FromCacheSize { get; set; }
        public int ToCacheSize { get; set; }

        public double FromCacheDifficulty { get; set; }
        public double ToCacheDifficulty { get; set; }

        public double FromTerrainDifficulty { get; set; }
        public double ToTerrainDifficulty { get; set; }

    }
}