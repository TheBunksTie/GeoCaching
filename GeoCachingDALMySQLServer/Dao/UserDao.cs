using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class UserDao : AbstractDaoBase, IUserDao {
        public UserDao(IDatabase database) : base(database) {}

        public List<User> GetAll() {
            return GetUserListFor(Database.CreateCommand(
                "SELECT u.id, u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id " +
                "ORDER BY u.name ASC;"));
        }

        public User GetByName(string name) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT u.id, u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id " +
                "WHERE u.name = @name;");
            Database.DefineParameter(cmd, "name", DbType.String, name);

            IList<User> list = GetUserListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public User GetById(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "SELECT u.id, u.name, u.password, u.email, u.latitude, u.longitude, lt.roleDescription " +
                "FROM user u INNER JOIN lt_user_role lt ON u.roleCode = lt.id " +
                "WHERE u.id = @id;");
            Database.DefineParameter(cmd, "id", DbType.Int32, id);

            IList<User> list = GetUserListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;
        }

        public List<string> GetAllUserRoles() {
            return GetRoleListFor(Database.CreateCommand("SELECT roleDescription FROM lt_user_role;"));
        }

        public List<string> GetPrivilegedRoles() {
            return GetRoleListFor(Database.CreateCommand("SELECT roleDescription FROM lt_user_role WHERE id > 1;"));
        }

        public int Insert(User user) {
            int roleCode = GetIdForRole(user.Role);

            IDbCommand cmd = Database.CreateCommand(
                "INSERT INTO user (name, password, email, latitude, longitude, roleCode) " +
                "VALUES (@name, @password, @email, @latitude, @longitude, @roleCode);");
            Database.DefineParameter(cmd, "name", DbType.String, user.Name);
            Database.DefineParameter(cmd, "password", DbType.String, user.PasswordHash);
            Database.DefineParameter(cmd, "email", DbType.String, user.Email);
            Database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            Database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            Database.DefineParameter(cmd, "roleCode", DbType.Int32, roleCode);

            if ( Database.ExecuteNonQuery(cmd) == 1 ) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = Database.CreateCommand("SELECT last_insert_id();");
                user.Id = ( int ) Database.ExecuteScalarQuery<long>(idCmd);
            }
            else {
                user.Id = -1;
            }
            return user.Id;            
        }

        public bool Update(User user) {
            int roleCode = GetIdForRole(user.Role);

            IDbCommand cmd = Database.CreateCommand(
                "UPDATE user SET name = @name, password = @password, email = @email, latitude =  @latitude, longitude = @longitude, roleCode = @roleCode " +
                "WHERE id = @id;");
            Database.DefineParameter(cmd, "name", DbType.String, user.Name);
            Database.DefineParameter(cmd, "password", DbType.String, user.PasswordHash);
            Database.DefineParameter(cmd, "email", DbType.String, user.Email);
            Database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            Database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            Database.DefineParameter(cmd, "roleCode", DbType.Int32, roleCode);
            // primary key
            Database.DefineParameter(cmd, "id", DbType.Int32, user.Id);

            return Database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(string userName) {
            IDbCommand cmd = Database.CreateCommand(
                "DELETE FROM user WHERE name = @name;");

            Database.DefineParameter(cmd, "name", DbType.String, userName);

            return Database.ExecuteNonQuery(cmd) == 1;
        }

        public bool DeleteById(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "DELETE FROM user WHERE id = @id;");

            Database.DefineParameter(cmd, "id", DbType.Int32, id);

            return Database.ExecuteNonQuery(cmd) == 1;
        }

        private int GetIdForRole(string role) {
            IDbCommand cmd = Database.CreateCommand("SELECT id FROM lt_user_role WHERE roleDescription = @role;");
            Database.DefineParameter(cmd, "role", DbType.String, role);

            return Database.ExecuteScalarQuery<int>(cmd);
        }

        private List<User> GetUserListFor(IDbCommand cmd) {
            using (IDataReader reader = Database.ExecuteReader(cmd)) {
                var users = new List<User>();

                while (reader.Read()) {
                    users.Add(new User {
                        Id = ( int ) reader["id"],
                        Name = reader["name"].ToString(),
                        PasswordHash = reader["password"].ToString(),
                        Email = reader["email"].ToString(),
                        Position = new GeoPosition(( double ) reader["latitude"], ( double ) reader["longitude"]),
                        Role = reader["roleDescription"].ToString()
                    });
                }
                return users;
            }
        }

        private List<string> GetRoleListFor(IDbCommand cmd) {
            using ( IDataReader reader = Database.ExecuteReader(cmd) ) {                
                List<string> roles = new List<string>();
                
                while ( reader.Read() ) {
                    roles.Add(( string ) reader["roleDescription"]);
                }
                return roles;
            }            
        } 
    }
}