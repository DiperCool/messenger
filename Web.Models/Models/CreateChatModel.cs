using System.ComponentModel.DataAnnotations;

namespace Web.Models.Models
{
    public class CreateChatModel
    {
        [Required]
        public string Name{get;set;}
    }
}