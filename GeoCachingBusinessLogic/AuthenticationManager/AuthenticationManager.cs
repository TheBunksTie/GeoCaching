using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.AuthenticationManager {
    public class AuthenticationManager : AbstractManagerBase, IAuthenticationManager {
        
        private readonly SHA1CryptoServiceProvider cryptoService = new SHA1CryptoServiceProvider();
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);
        private bool isAuthenticated = false;

        public User AuthenticateUser(string username, string password, bool priviligedRequired) {
            
            // hash password
            string passwordHash = EncodePasswordToBase64(password);

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

        private string EncodePasswordToBase64 ( string password ) {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = cryptoService.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}