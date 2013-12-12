using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.AuthenticationManager {
    public interface IAuthenticationManager {
        bool IsAuthenticated { get; }
        
        User AuthenticateUser(string username, string password, bool priviligedRequired);

        void LogoutUser();
    }
}