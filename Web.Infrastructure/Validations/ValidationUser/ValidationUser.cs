using System.Linq;
using Web.Models.Db;

namespace Web.Infrastructure.Validations.ValidationUser
{
    public class ValidationUser:IValidationUser
    {
        Context _context;
        public ValidationUser(Context context)
        {
            _context = context;
        }
        public bool userIsExist(string login)
        {
            return _context.Users.Any(x => x.Login == login);
        }
    }
}