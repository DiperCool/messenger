using System;
using System.Collections.Generic;
using Web.Models.Entity;

namespace Web.Models.ModelsDTO
{
    public class MessageDTO
    {
        public int Id{get;set;}
        public string Content{get;set;}
        public List<MediaDTO> Medias{get;set;}= new List<MediaDTO>();
        public DateTime Created{get;set;} = DateTime.Now;
        public UserDTO Creator{get;set;}
    }
}