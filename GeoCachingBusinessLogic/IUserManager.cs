using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic {
    public interface IUserAdmin {
        bool CreateUser(User u);
        bool EditUser(User u);
        bool DeleteUser(User u);
    }
}