using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Entity;

namespace Web.Infrastructure.UserServices
{
    public interface IUserService
    {
        Task<User> GetUser(string login);
        Task AddNewAva(Media media, string login);
        Task setUserStatus(string login, bool isOnline);
        Task<List<Connection>> GetConnections(string login);
        Task<int> CountConnectionsUser(string login);
        Task RemoveConnection(string con);
        Task AddConnection(string con, string login);
        Task<bool> UserIsExist(string login);
    }
}