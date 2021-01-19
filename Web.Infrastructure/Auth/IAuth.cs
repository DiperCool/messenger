using Web.Models.Entity;

namespace Web.Infrastructure.Auth
{
    public interface IAuth
    {
        User GetUser(string login,string password);
        User GetUser(string login);
        bool CheckUserHaveRefreshToken(string login, string refreshToken);
        void SaveRefreshToken(string login,string token);
        void CreateUser(User user); 
        void SetRefreshToken(string login,string token);
        string GetRefreshToken(string login);
    }
}