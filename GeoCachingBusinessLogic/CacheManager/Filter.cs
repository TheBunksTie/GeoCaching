using System;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public class Filter {
        public GeoPosition FromPosition { get; set; }
        public GeoPosition ToPosition { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}