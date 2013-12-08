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

        public void UpdateExistingUser(User u) {
            // validate user input concerning empty fields
            ValidateUserInput(u);

            if (userDao.GetById(u.Id) != null) {
                // TODO fix bug
                // name must be unique, so check if user has not choosen an already exisiting name
                if (userDao.GetByName(u.Name).Id == u.Id) {
                    userDao.Update(u);
                }
                else {
                    throw new Exception("Error: The username " + u.Name + " has already been taken.");
                }
            }
            else {
                throw new Exception("Error: User " + u.Name + " is not exisiting in databse.");
            }
        }

        public User CreateNewDefaultUser() {
            const string defaultName = "<new user>";
            User u = null;

            // check if there is another "default" user with default name in database
            if (userDao.GetByName(defaultName) == null) {
                u = new User(-1, defaultName, "", "my.mail@domain.com", new GeoPosition(47.123, 18.123), "Finder");
                userDao.Insert(u);
            }
            else {
                throw new Exception("Error: A user with name " + defaultName + " is already in database.");
            }
            return u;
        }

        public void DeleteUser(int id) {
            // TODO check for ownership of cache, logs, ratings
            userDao.DeleteById(id);
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