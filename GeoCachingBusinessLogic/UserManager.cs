using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic {
    public class UserManager : IUserManager {
        private static readonly IDatabase database = DalFactory.CreateDatabase();
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);

        public List<User> GetUsers() {
            return userDao.GetAll();
        }

        public bool CreateUser(User u) {
            return userDao.Insert(u);
        }

        public bool EditUser(User u) {
            return userDao.Update(u);
        }

        public bool DeleteUser(User u) {
            return true;
        }
    }
}