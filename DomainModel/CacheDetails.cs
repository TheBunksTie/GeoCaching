using System.Collections.Generic;

namespace Swk5.GeoCaching.DomainModel {
    public class CacheDetails {
        public Cache Cache { get; set; }
        public List<Image> Images { get; set; }
        public double Rating { get; set; }
        public List<LogEntry> LogEntries { get; set; }
    }
}