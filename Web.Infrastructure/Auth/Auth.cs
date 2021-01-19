using System.Linq;
using Microsoft.EntityFrameworkCore;
using Web.Models.Db;
using Web.Models.Entity;

namespace Web.Infrastructure.Auth
{
    public class Auth: IAuth
    {
        private Context _context;
        public Auth(Context context)
        {
            _context=context;
        }

        public bool CheckUserHaveRefreshToken(string login, string refreshToken)
        {
            User user= _context.Users.FirstOrDefault(x=>x.RefreshToken==refreshToken&&x.Login==login);
            if(user==null) return false;
            return true;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUser(string login, string password)
        {
            User user = _context.Users.FirstOrDefault(x=>x.Login==login&&x.Password==password);
            return user;
        }public User GetUser(string login)
        {
            User user = _context.Users
                .Include(x=>x.CurrentAva)
                .FirstOrDefault(x=>x.Login==login);
            return user;
        }

        public void SaveRefreshToken(string login, string token)
        {
            User user= _context.Users.FirstOrDefault(x=>x.Login==login);
            if(user==null) return;
            user.RefreshToken=token;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void SetRefreshToken(string login, string token)
        {
            User user= _context.Users.FirstOrDefault(x=>x.Login==login);
            user.RefreshToken = token;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public string GetRefreshToken(string login)
        {
            return _context.Users.FirstOrDefault(x => x.Login == login)?.RefreshToken;
        }
    }
}