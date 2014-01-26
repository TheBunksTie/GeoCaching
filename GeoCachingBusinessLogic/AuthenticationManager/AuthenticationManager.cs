using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.AuthenticationManager {
    public class AuthenticationManager : AbstractManagerBase, IAuthenticationManager {
        private static readonly IUserDao userDao = DalFactory.CreateUserDao(database);
        private static User authenticatedUser;

        public User AuthenticateUser(string username, string password, LoginMode mode) {
            // reset authentication indicator
            authenticatedUser = null;

            // encrypt password
            string passwordHash = password.Encrypt();

            // look for requested user
            User user = userDao.GetByName(username);

            // checks if all conditions for access are fullfilled
            if (user != null) {
                // check if a special privilege is required to login 
                if (mode == LoginMode.PrivilegeRequired) {
                    List<string> privilegedRoles = userDao.GetPrivilegedRoles();

                    if (!privilegedRoles.Contains(user.Role)) {
                        return null;
                    }
                }

                if (user.PasswordHash.Equals(passwordHash)) {
                    authenticatedUser = user;
                }
            }
            return authenticatedUser;
        }

        // reauthenticate user by comparing password hashes
        public bool ReauthenticateUser(User u) {
            User expectedUser = userDao.GetById(u.Id);
            return (expectedUser != null && u.PasswordHash.Equals(expectedUser.PasswordHash));
        }

        public void LogoutUser() {
            authenticatedUser = null;
        }

        public User AuthenticatedUser {
            get { return authenticatedUser; }
        }
    }
}