using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.AuthenticationManager {
    public class AuthenticationManager : AbstractManagerBase, IAuthenticationManager {
        private static readonly IUserDao userDao = DalFactory.CreateUserDao(database);
        private static User authenticatedUser;

        public User AuthenticateUser(string username, string password, bool priviligedRequired) {
            // reset authentication indicator
            authenticatedUser = null;
            
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
                    authenticatedUser = u;
                    return u;
                }
            }
            return null;
        }

        // reauthenticate user by comparing password hashes
        public bool ReauthenticateUser(User u) {
            User expectedUser = userDao.GetById(u.Id);
            return (expectedUser != null && u.Password.Equals(expectedUser.Password));
        }

        public void LogoutUser() {
            authenticatedUser = null;
        }

        public User AuthenticatedUser {
            get { return authenticatedUser; }
        }
    }
}