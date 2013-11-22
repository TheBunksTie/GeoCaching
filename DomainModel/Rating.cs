﻿using System;

namespace Swk5.GeoCaching.DomainModel {
    public class Rating : IEquatable<Rating> {
        
        public Rating(int cacheId, string creator, DateTime creationDate, int grade) {
            CacheId = cacheId;
            Creator = creator;
            CreationDate = creationDate;
            Grade = grade;
        }

        public Rating ( ) {
        }

        public bool Equals(Rating other) {
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
            if (obj.GetType() != this.GetType()) {
                return false;
            }
            return Equals(( Rating ) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (CacheId*397) ^ (Creator != null ? Creator.GetHashCode() : 0);
            }
        }

        public int CacheId { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public int Grade { get; set; }

    }
}
