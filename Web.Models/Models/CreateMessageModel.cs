using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Web.Models.interfaces;

namespace Web.Models.Models
{
    public class CreateMessageModel:IChatIsNull
    {
        [Required]
        public string Content{get;set;}
        public List<IFormFile> Files= new List<IFormFile>();
        [Required]
        public string guid{get;set;}
    }
}