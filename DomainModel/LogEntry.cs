using System;

namespace Swk5.GeoCaching.DomainModel {
    public class LogEntry : IEquatable<LogEntry> {
        public LogEntry() {}

        public LogEntry(int id, int cacheId, int creatorId, DateTime creationDate, bool isFound, string comment) {
            Id = id;
            CacheId = cacheId;
            CreatorId = creatorId;
            CreationDate = creationDate;
            IsFound = isFound;
            Comment = comment;
        }

        public int Id { get; set; }
        public int CacheId { get; set; }
        public int CreatorId { get; set; }
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
            return Id == other.Id;
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
            return Id;
        }
    }
}