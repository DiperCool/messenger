using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web.Models.Models
{
    public class NewAvaModel
    {
        [Required]
        public IFormFile file{get;set;}
    }
}