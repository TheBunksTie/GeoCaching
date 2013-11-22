using System;

namespace Swk5.GeoCaching.DomainModel {
    public enum UserRole {
        Finder,
        Hider,
        FinderHider
    }

    public class User {
        public User(string name,
            string password,
            string email,
            GeoPosition position,
            int role,
            DateTime registrationDate) {
            Name = name;
            Password = password;
            Email = email;
            Position = position;
            Role = GetUserRolesById(role);
            RegistrationDate = registrationDate;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public GeoPosition Position { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegistrationDate { get; set; }

        public int GetUserRoleAsId() {
            return ( int ) Role;
        }

        private UserRole GetUserRolesById(int id) {
            return ( UserRole ) id;
        }
    }
}