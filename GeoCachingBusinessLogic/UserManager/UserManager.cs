using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.UserManager {
    public class UserManager : AbstractManagerBase, IUserManager {       
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);
        private readonly ILogEntryDao logEntryDao = DalFactory.CreateLogEntryDao(database);
        private readonly IRatingDao ratingDao = DalFactory.CreateRatingDao(database);
        private readonly ICacheDao cacheDao = DalFactory.CreateCacheDao(database);
        
        public List<User> GetUserList() {
            return userDao.GetAll();
        }

        public List<string> GetUserRoleList() {
            return userDao.GetAllUserRoles();
        }

        public bool UpdateExistingUser(User u, bool passwordChanged) {
            // encrypt password only if changed (if not pw is still hashed)
            if (passwordChanged) {
                u.PasswordHash = u.PasswordHash.Encrypt();
            }
            // validate user input concerning empty fields
            ValidateUser(u);

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

            // check if there is another "default" user with default name in database
            if (userDao.GetByName(defaultName) == null) {
                User u = new User { Id = -1, Name = defaultName, PasswordHash = "".Encrypt(), Email = "my.mail@domain.com", Position = new GeoPosition(48.363889, 14.519444), Role = "Finder" };
                
                // inserts user and updates input user object with assigned id
                userDao.Insert(u);
                return u;
            }
            throw new Exception("Error: A user with name " + defaultName + " is already in database.");
        }

        public bool DeleteUser(int id) {
            bool isDeleteable = (cacheDao.GetByOwner(id).Count == 0);
            isDeleteable = isDeleteable && (logEntryDao.GetLogentriesForUser(id).Count == 0);
            isDeleteable = isDeleteable && (ratingDao.GetRatingsForUser(id).Count == 0);

            if (isDeleteable) {
                return userDao.DeleteById(id);                
            }           
            throw new Exception("Error: Unable to delete selected user. User owns caches and/or log entries and/or rating entries.");
        }

        private void ValidateUser(User u) {
            if (u.Name.Length < 1) {
                throw new Exception("Error: User name must no be empty.");
            }
           
            if (u.Role.Length < 1) {
                throw new Exception("Error: User role must not be empty.");
            }

            if (u.Email.Length < 1) {
                throw new Exception("Error: User email address must not be empty.");
            }
            
            if (u.Position.Latitude < 0 && u.Position.Longitude < 0) {
                throw new Exception("Error: Position is invalid.");
            }
        }
    }
}