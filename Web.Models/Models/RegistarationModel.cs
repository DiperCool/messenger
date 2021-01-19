using System.ComponentModel.DataAnnotations;

namespace Web.Models.Models
{
    public class RegistarationModel
    {
        [Required(ErrorMessage="Логин не должен быть пустым")]
        [StringLength(10, MinimumLength=3, ErrorMessage="Логин должен быть от 3 до 10 символов")]
        public string Login{get;set;}
        [Required(ErrorMessage="Пароль не должен быть пустым")]
        public string Password{get;set;}
        [Compare("Password", ErrorMessage="Пароли не совпадают")]
        public string RePassword{get;set;}
    }
}