using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.AuthenticationManager {
    public class AuthenticationManager : AbstractManagerBase, IAuthenticationManager {
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);
        private bool isAuthenticated;

        public User AuthenticateUser(string username, string password, bool priviligedRequired) {
            // hash password
            string passwordHash = password.Encrypt();

            // look for requested user
            User u = userDao.GetByName(username);

            // do the checks
            if (u != null) {
                // check if a special privilege is required to login 
                if (priviligedRequired) {
                    List<string> privilegedRoles = userDao.GetPrivilegedRoles();

                    if (!privilegedRoles.Contains(u.Role)) {
                        return null;
                    }
                }

                if (u.Password.Equals(passwordHash)) {
                    isAuthenticated = true;
                    return u;
                }
            }
            return null;
        }

        public void LogoutUser() {
            isAuthenticated = false;
        }

        public bool IsAuthenticated {
            get { return isAuthenticated; }
        }
    }
}