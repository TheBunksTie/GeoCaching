using System.Collections.Generic;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.UserManager {
    public interface IUserManager {
        // retrieve different user data from database
        List<User> GetUsers();
        List<string> GetUserRoles();

        // creates a user with default name and settings and trys to put him into db
        User CreateNewDefaultUser();

        // trys to find specified user in db and the update him
        void UpdateExistingUser(User u);

        // deletes the user 
        void DeleteUser(int id);
    }
}