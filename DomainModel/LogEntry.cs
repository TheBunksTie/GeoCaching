using System;

namespace Swk5.GeoCaching.DomainModel {
    public class LogEntry : IEquatable<LogEntry> {
        public LogEntry() {}

        public LogEntry(int cacheId, string creator, DateTime creationDate, bool isFound, string comment) {
            CacheId = cacheId;
            Creator = creator;
            CreationDate = creationDate;
            IsFound = isFound;
            Comment = comment;
        }

        public int CacheId { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsFound { get; set; }
        public string Comment { get; set; }

        public bool Equals(LogEntry other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return CacheId == other.CacheId && string.Equals(Creator, other.Creator);
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
            return Equals(( LogEntry ) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (CacheId*397) ^ (Creator != null ? Creator.GetHashCode() : 0);
            }
        }
    }
}