using System;

namespace Swk5.GeoCaching.DomainModel {
    public enum UserRole {
        Finder = 1,
        Hider = 2,
        FinderHider = 3,
        Inactive = 4
    }

    public class User : IEquatable<User> {
        private int roleCode;

        public User(string name, string password, string email, GeoPosition position, int role, DateTime registrationDate) {
            Name = name;
            Password = password;
            Email = email;
            Position = position;
            RoleCode = role;
            RegistrationDate = registrationDate;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public GeoPosition Position { get; set; }

        public UserRole Role {
            get { return ( UserRole ) roleCode; }
            set { roleCode = ( int ) value; }
        }

        public int RoleCode {
            get { return roleCode; }
            set {
                if (value >= 1 && value <= 4) {
                    roleCode = value;
                }
            }
        }

        public DateTime RegistrationDate { get; set; }

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
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}