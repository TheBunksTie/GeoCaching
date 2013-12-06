﻿using System;

namespace Swk5.GeoCaching.DomainModel {
    public class Rating : IEquatable<Rating> {
        private int grade;

        public Rating(int id, int cacheId, int creatorId, DateTime creationDate, int grade) {
            Id = id;
            CacheId = cacheId;
            CreatorId = creatorId;
            CreationDate = creationDate;
            Grade = grade;
        }

        public Rating() {}

        public int Id { get; set; }
        public int CacheId { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationDate { get; set; }

        public int Grade {
            get { return grade; }
            set {
                if (value >= 1 && value <= 10) {
                    grade = value;
                }
                else {
                    throw new ArgumentException();
                }
            }
        }

        public bool Equals(Rating other) {
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
            return Equals(( Rating ) obj);
        }

        public override int GetHashCode() {
            return Id;
        }
    }
}