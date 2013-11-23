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
        private double cacheDifficulty;
        private int cacheSize;
        private double terrainDifficulty;

        public Cache ( int id, string name, DateTime creationDate, double cacheDifficulty, double terrainDifficulty, int size, string owner, GeoPosition position, string description ) {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            CacheDifficulty = cacheDifficulty;
            TerrainDifficulty = terrainDifficulty;
            SizeCode = size;
            Owner = owner;
            Position = position;
            Description = description;
        }

        public Cache() {}

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public double CacheDifficulty {
            get { return cacheDifficulty; }
            set {
                if (value >= 0 && value <= 5) {
                    cacheDifficulty = value;
                }
            }
        }

        public double TerrainDifficulty {
            get { return terrainDifficulty; }
            set {
                if (value >= 0 && value <= 5) {
                    terrainDifficulty = value;
                }
            }
        }

        public CacheSize Size {
            get { return ( CacheSize ) cacheSize; }
            set { cacheSize = ( int ) value; }
        }

        public int SizeCode {
            get { return cacheSize; }
            set {
                if (value >= 1 && value <= 6) {
                    cacheSize = value;
                }
            }
        }

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

        public override int GetHashCode() {
            return Id;
        }
    }
}