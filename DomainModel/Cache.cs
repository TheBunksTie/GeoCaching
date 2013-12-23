using System;

namespace Swk5.GeoCaching.DomainModel {
    public class Cache : IEquatable<Cache> {
        private double cacheDifficulty;
        private double terrainDifficulty;

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public double CacheDifficulty {
            get { return cacheDifficulty; }
            set {
                if (value >= 1 && value <= 5) {
                    cacheDifficulty = value;
                }
                else {
                    throw new ArgumentException();
                }
            }
        }

        public double TerrainDifficulty {
            get { return terrainDifficulty; }
            set {
                if (value >= 1 && value <= 5) {
                    terrainDifficulty = value;
                }
                else {
                    throw new ArgumentException();
                }
            }
        }

        public string Size { get; set; }
        public int OwnerId { get; set; }
        public GeoPosition Position { get; set; }
        public string Description { get; set; }

        public bool Equals(Cache other) {
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
            return Equals(( Cache ) obj);
        }

        public override string ToString() {
            return string.Format("Id: {0}, Name: {1}, size: {2}", Id, Name, Size);
        }

        public override int GetHashCode() {
            return Id;
        }
    }

    public enum FilterCriterium {
        Size,
        TerrainDifficulty,
        CacheDifficulty
    }

    public enum FilterOperation {
        Exact,
        Above,
        AboveEquals,
        Below,
        BelowEquals
    }

    public static class EnumExtension {
        public static string UiCaption ( this FilterCriterium c ) {
            if ( c == FilterCriterium.Size ) {
                return "by size";
            }
            if ( c == FilterCriterium.CacheDifficulty ) {
                return "by cache difficulty";
            }
            if ( c == FilterCriterium.TerrainDifficulty ) {
                return "by terrain difficulty";
            }

            throw new ArgumentException();
        }

        public static string UiCaption ( this FilterOperation c ) {
            if ( c == FilterOperation.Below ) {
                return "less than";
            }
            if ( c == FilterOperation.BelowEquals ) {
                return "less or equal than";
            }
            if ( c == FilterOperation.Above ) {
                return "greater than";
            }
            if ( c == FilterOperation.AboveEquals ) {
                return "greater or equal than";
            }
            if ( c == FilterOperation.Exact ) {
                return "equals";
            }
            throw new ArgumentException();
        }
    }

}