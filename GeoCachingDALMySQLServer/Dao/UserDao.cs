using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class UserDao : AbstractDao, IUserDao {
        public UserDao(IDatabase database) : base(database) {}

        public List<User> GetAll() {
            return GetUserListFor(database.CreateCommand(
                "SELECT u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription, u.registrationDate " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id;"));
        }

        public User GetByName(string name) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription, u.registrationDate " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id " +
                "WHERE u.name = @name;");
            database.DefineParameter(cmd, "name", DbType.String, name);

            IList<User> list = GetUserListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<string> GetAllUserRoles() {
            List<string> roles = new List<string>();

            IDbCommand cmd = database.CreateCommand("SELECT roleDescription FROM lt_user_role;");

            using ( IDataReader reader = database.ExecuteReader(cmd) ) {
                while ( reader.Read() ) {
                    roles.Add((string) reader["roleDescription"]);
                }
            }
            return roles;
        }

        public bool Insert(User user) {
            int roleCode = GetIdForRole(user.Role);

            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO user (name, password, email, latitude, longitude, roleCode, registrationDate) " +
                "VALUES (@name, @password, @email, @latitude, @longitude, @roleCode, @registrationDate);");
            database.DefineParameter(cmd, "name", DbType.String, user.Name);
            database.DefineParameter(cmd, "password", DbType.String, user.Password);
            database.DefineParameter(cmd, "email", DbType.String, user.Email);
            database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            database.DefineParameter(cmd, "roleCode", DbType.Int32, roleCode);
            database.DefineParameter(cmd, "registrationDate", DbType.Date, user.RegistrationDate);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(User user) {
            int roleCode = GetIdForRole(user.Role);

            IDbCommand cmd = database.CreateCommand(
                "UPDATE user SET password = @password, email = @email, latitude =  @latitude, longitude = @longitude, roleCode = @roleCode, registrationDate = @registrationDate " +
                "WHERE name = @name;");
            database.DefineParameter(cmd, "password", DbType.String, user.Password);
            database.DefineParameter(cmd, "email", DbType.String, user.Email);
            database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            database.DefineParameter(cmd, "roleCode", DbType.Int32, roleCode);
            database.DefineParameter(cmd, "registrationDate", DbType.Date, user.RegistrationDate);
            // primary key
            database.DefineParameter(cmd, "name", DbType.String, user.Name);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(string userName) {
            IDbCommand cmd = database.CreateCommand(
                "DELETE FROM user WHERE name = @name;");

            database.DefineParameter(cmd, "name", DbType.String, userName);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        private int GetIdForRole(string role) {
            IDbCommand cmd = database.CreateCommand("SELECT id FROM lt_user_role WHERE roleDescription = @role;");
            database.DefineParameter(cmd, "role", DbType.String, role);

            return database.ExecuteScalarQuery<int>(cmd);
        }

        private List<User> GetUserListFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                List<User> users = new List<User>();

                while (reader.Read()) {
                    users.Add(new User(
                        ( string ) reader["name"],
                        ( string ) reader["password"],
                        ( string ) reader["email"],
                        new GeoPosition(( double ) reader["latitude"], ( double ) reader["longitude"]), 
                        (string) reader["roleDescription"],
                        DateTime.Parse(reader["registrationDate"].ToString())));
                }
                return users;
            }
        }
    }
}