using System;
using System.Collections.Generic;

namespace Swk5.GeoCaching.DomainModel {
    public enum CacheSize {
        Micro = 1,
        Small = 2,
        Regular = 3,
        Large = 4,
        Other = 5,
        None = 6
    }

    public class Cache : IEquatable<Cache> {
        private readonly IList<string> assignedImages = new List<string>();

        public Cache(int id,
            string name,
            DateTime creationDate,
            double difficultyCache,
            double difficultyTerrain,
            int size,
            string owner,
            GeoPosition position,
            string description) {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            DifficultyCache = difficultyCache;
            DifficultyTerrain = difficultyTerrain;
            Size = GetCacheSizeForId(size);
            Owner = owner;
            Position = position;
            Description = description;
        }

        public Cache() {}

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public double DifficultyCache { get; set; }
        public double DifficultyTerrain { get; set; }
        public CacheSize Size { get; set; }
        public string Owner { get; set; }
        public GeoPosition Position { get; set; }
        public string Description { get; set; }

        public IList<string> AssignesImages {
            get { return assignedImages; }
        }

        public bool Equals(Cache other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Id == other.Id;
        }

        public override string ToString() {
            return string.Format("Id: {0}, Name: {1}, Size: {2}", Id, Name, Size);
        }

        public void AddAssignedImage(string fileName) {
            assignedImages.Add(fileName);
        }

        public int GetCacheSizeAsId() {
            return ( int ) Size;
        }

        public override int GetHashCode() {
            return Id;
        }

        private CacheSize GetCacheSizeForId(int id) {
            return ( CacheSize ) id;
        }
    }
}