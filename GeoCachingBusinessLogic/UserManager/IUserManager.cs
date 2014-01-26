using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.UserManager {
    public interface IUserManager {
        // retrieve list of all existing users
        List<User> GetUserList();
        
        // retrieve lookup list of all defined user roles
        List<string> GetUserRoleList();

        // creates a user with default name and settings and trys to put him into db
        User CreateNewDefaultUser();

        // trys to find specified user in db and the update him
        bool UpdateExistingUser(User u, bool passwordChanged);

        // deletes the user 
        bool DeleteUser(int id);
    }
}