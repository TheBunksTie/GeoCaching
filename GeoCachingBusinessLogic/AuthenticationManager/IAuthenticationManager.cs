using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.AuthenticationManager {

    public enum LoginMode {
        generalAccessible,
        privilegeRequired
    }
    
    public interface IAuthenticationManager {
        User AuthenticatedUser { get; }
        
        User AuthenticateUser(string username, string password, bool priviligedRequired);
        void LogoutUser();
    }
}