using System;

namespace Swk5.GeoCaching.DomainModel {
    public class User : IEquatable<User> {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public GeoPosition Position { get; set; }

        public string Role { get; set; }

        public bool Equals(User other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return string.Equals(Name, other.Name);
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
            return Equals(( User ) obj);
        }

        public override int GetHashCode() {
            return Name.GetHashCode();
        }
    }
}