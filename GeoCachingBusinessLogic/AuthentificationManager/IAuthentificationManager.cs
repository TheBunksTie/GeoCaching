namespace Swk5.GeoCaching.BusinessLogic.AuthentificationManager {
    public interface IAuthentificationManager {
        bool LoginUser(string username, string password);
        bool LogoutUser(string username);

        bool IsAuthenticated(string username);
    }
}