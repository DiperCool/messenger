using System.ComponentModel.DataAnnotations;
using Web.Models.interfaces;

namespace Web.Models.Models
{
    public class AddToChatModel:IUserIsNull,IChatIsNull
    {
        [Required]
        public string guid{get;set;}
        [Required]
        public string Login{get;set;}
    }
}