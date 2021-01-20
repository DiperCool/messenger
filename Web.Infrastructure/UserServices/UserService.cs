using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models.Db;
using Web.Models.Entity;

namespace Web.Infrastructure.UserServices
{
    public class UserService : IUserService
    {
        private Context _context;

        public UserService(Context context)
        {
            _context = context;
        }
        
        public async Task AddNewAva(Media media, string login)
        {
            User user =await _context.Users.FirstOrDefaultAsync(x=>x.Login==login);
            user.Avas.Add(media);
            user.CurrentAva=media;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Connection>> GetConnections(string login)
        {
            return await _context.Connections
                            .Where(x=>x.Login==login)
                            .ToListAsync();
        }

        public async Task<User> GetUser(string login)
        {
            return await _context.Users
                        .Include(x=>x.CurrentAva)
                        .FirstOrDefaultAsync(x=>x.Login==login);
        }

        public async Task setUserStatus(string login, bool isOnline)
        {
            User user = await GetUser(login);
            user.isOnline=isOnline;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddConnection(string con, string login)
        {
            var connection= new Connection{ConnectionString=con, Login=login};
            await _context.Connections.AddAsync(connection);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveConnection(string con)
        {
            var connection = await _context.Connections
                            .Where(x=>x.ConnectionString==con)
                            .FirstOrDefaultAsync();
            _context.Connections.Remove(connection);
            await  _context.SaveChangesAsync();
        }
        public async Task<int> CountConnectionsUser(string login)
        {
            return await _context.Connections
                            .Where(x=>x.Login==login)
                            .CountAsync();
        }

        public async Task<bool> UserIsExist(string login)
        {
            return await _context.Users.AnyAsync(x=>x.Login==login);
        }
    }
}