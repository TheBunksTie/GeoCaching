using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class UserDao : AbstractDao, IUserDao {
        public UserDao(IDatabase database) : base(database){
        }

        public IList<User> GetAll() {
            return GetUserListFor(database.CreateCommand(
                "SELECT u.name, u.password, u.email, u.latitude, u.longitude, u.roleId, u.registrationDate " +
                "FROM user u;"));
        }

        public User GetByName(string name) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT u.name, u.password, u.email, u.latitude, u.longitude, u.roleId, u.registrationDate " +
                "FROM user u WHERE u.name = @name");
            database.DefineParameter(cmd, "name", DbType.String, name);

            IList<User> list = GetUserListFor(cmd);

            if (list.Count == 1) {
                return list[0];
            }
            return null;    
        }

        public bool Insert(User user) {
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO user (name, password, email, latitude, longitude, roleId, registrationDate) " +
                "VALUES (@name, @password, @email, @latitude, @longitude, @roleId, @registrationDate);");
            database.DefineParameter(cmd, "name", DbType.String, user.Name);
            database.DefineParameter(cmd, "password", DbType.String, user.Password);
            database.DefineParameter(cmd, "email", DbType.String, user.Email);
            database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            database.DefineParameter(cmd, "roleId", DbType.Int32, user.GetUserRoleAsId());
            database.DefineParameter(cmd, "registrationDate", DbType.Date, user.RegistrationDate);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(User user) {
            IDbCommand cmd = database.CreateCommand(
                "UPDATE user SET password = @password, email = @email, latitude =  @latitude, longitude = @longitude, roleId = @roleId, registrationDate = @registrationDate " +
                "WHERE name = @name");
            database.DefineParameter(cmd, "password", DbType.String, user.Password);
            database.DefineParameter(cmd, "email", DbType.String, user.Email);
            database.DefineParameter(cmd, "latitude", DbType.Double, user.Position.Latitude);
            database.DefineParameter(cmd, "longitude", DbType.Double, user.Position.Longitude);
            database.DefineParameter(cmd, "roleId", DbType.Int32, user.GetUserRoleAsId());
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

        private IList<User> GetUserListFor(IDbCommand cmd) {
            using ( IDataReader reader = database.ExecuteReader(cmd) ) {
                IList<User> users = new List<User>();

                while ( reader.Read() ) {
                    users.Add(new User(
                        ( string ) reader["name"],
                        ( string ) reader["password"],
                        ( string ) reader["email"],
                        new GeoPosition(( double ) reader["latitude"], ( double ) reader["longitude"]), 
                        ( int ) reader["roleId"],
                        DateTime.Parse(reader["registrationDate"].ToString())));
                }
                return users;
            }
        }
    }
}