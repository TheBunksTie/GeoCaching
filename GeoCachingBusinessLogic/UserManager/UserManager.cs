using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.UserManager {
    public class UserManager : AbstractManagerBase, IUserManager {       
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);

        public List<User> GetUserList() {
            return userDao.GetAll();
        }

        public List<string> GetUserRoleList() {
            return userDao.GetAllUserRoles();
        }

        public bool UpdateExistingUser(User u) {
            // encrypt password
            u.Password = u.Password.Encrypt();

            // validate user input concerning empty fields
            ValidateUserInput(u);

            if (userDao.GetById(u.Id) != null) {
                // name must be unique, so check if user has not choosen an already exisiting name
                User expectedUser = userDao.GetByName(u.Name);
                if (expectedUser == null || expectedUser.Id == u.Id) {
                    return userDao.Update(u);
                }
                throw new Exception("Error: The username " + u.Name + " has already been taken.");                
            }            
            throw new Exception("Error: User " + u.Name + " is not exisiting in databse.");           
        }

        public User CreateNewDefaultUser() {
            const string defaultName = "<new user>";
            User u = null;

            // check if there is another "default" user with default name in database
            if (userDao.GetByName(defaultName) == null) {
                u = new User{Id = -1, Name = defaultName, Password = "".Encrypt(), Email = "my.mail@domain.com", Position = new GeoPosition(47.123, 18.123), Role = "Finder"};
                userDao.Insert(u);
            }
            else {
                throw new Exception("Error: A user with name " + defaultName + " is already in database.");
            }
            return u;
        }

        public bool DeleteUser(int id) {
            // TODO check for ownership of cache, logs, ratings
            return userDao.DeleteById(id);
        }

        private void ValidateUserInput(User u) {
            if (u.Name == "") {
                throw new Exception("Error: User name must no be empty.");
            }

            if (u.Password.Length < 3) {
                throw new Exception("Error: Password is not adequate. (3 chars or more)");
            }

            if (u.Role == "") {
                throw new Exception("Error: User role must not be empty.");
            }

            if (u.Email == "") {
                throw new Exception("Error: User email address must not be empty.");
            }

            if (u.Position.Latitude < 0 && u.Position.Longitude < 0) {
                throw new Exception("Error: Position is invalid.");
            }
        }
    }
}