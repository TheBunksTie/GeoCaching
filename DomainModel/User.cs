using System;

namespace Swk5.GeoCaching.DomainModel {

    public class User : IEquatable<User> {
        public User(int id, string name, string password, string email, GeoPosition position, string role) {
            Id = id;
            Name = name;
            Password = password;
            Email = email;
            Position = position;
            Role = role;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
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