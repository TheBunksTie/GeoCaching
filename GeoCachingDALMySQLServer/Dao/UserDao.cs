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
                "SELECT u.id, u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id;"));
        }

        public User GetByName(string name) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT u.id, u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id " +
                "WHERE u.name = @name;");
            database.DefineParameter(cmd, "name", DbType.String, name);

            IList<User> list = GetUserListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public User GetById(int id) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT u.id, u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id " +
                "WHERE u.id = @id;");
            database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<User> list = GetUserListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<string> GetAllUserRoles() {
            var roles = new List<string>();

            IDbCommand cmd = database.CreateCommand("SELECT roleDescription FROM lt_user_role;");

            using (IDataReader reader = database.ExecuteReader(cmd)) {
                while (reader.Read()) {
                    roles.Add(( string ) reader["roleDescription"]);
                }
            }
            return roles;
        }

        public bool Insert(User user) {
            int roleCode = GetIdForRole(user.Role);

            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO user (name, password, email, latitude, longitude, roleCode) " +
                "VALUES (@name, @password, @email, @latitude, @longitude, @roleCode);");
            database.DefineParameter(cmd, "name", DbType.String, user.Name);
            database.DefineParameter(cmd, "password", DbType.String, user.Password);
            database.DefineParameter(cmd, "email", DbType.String, user.Email);
            database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            database.DefineParameter(cmd, "roleCode", DbType.Int32, roleCode);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(User user) {
            int roleCode = GetIdForRole(user.Role);

            IDbCommand cmd = database.CreateCommand(
                "UPDATE user SET name = @name, password = @password, email = @email, latitude =  @latitude, longitude = @longitude, roleCode = @roleCode " +
                "WHERE id = @id;");
            database.DefineParameter(cmd, "name", DbType.String, user.Name);
            database.DefineParameter(cmd, "password", DbType.String, user.Password);
            database.DefineParameter(cmd, "email", DbType.String, user.Email);
            database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            database.DefineParameter(cmd, "roleCode", DbType.Int32, roleCode);
            // primary key
            database.DefineParameter(cmd, "id", DbType.Int32, user.Id);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(string userName) {
            IDbCommand cmd = database.CreateCommand(
                "DELETE FROM user WHERE name = @name;");

            database.DefineParameter(cmd, "name", DbType.String, userName);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool DeleteById(int id) {
            IDbCommand cmd = database.CreateCommand(
                "DELETE FROM user WHERE id = @id;");

            database.DefineParameter(cmd, "id", DbType.Int32, id);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        private int GetIdForRole(string role) {
            IDbCommand cmd = database.CreateCommand("SELECT id FROM lt_user_role WHERE roleDescription = @role;");
            database.DefineParameter(cmd, "role", DbType.String, role);

            return database.ExecuteScalarQuery<int>(cmd);
        }

        private List<User> GetUserListFor(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                var users = new List<User>();

                while (reader.Read()) {
                    users.Add(new User(
                        ( int ) reader["id"],
                        ( string ) reader["name"],
                        ( string ) reader["password"],
                        ( string ) reader["email"],
                        new GeoPosition(( double ) reader["latitude"], ( double ) reader["longitude"]),
                        ( string ) reader["roleDescription"]));
                }
                return users;
            }
        }
    }
}