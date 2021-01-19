using System;
using System.Collections.Generic;

namespace Web.Models.Entity
{
    public class Message
    {
        public int Id{get;set;}
        public string Content{get;set;}
        public List<Media> Medias{get;set;}= new List<Media>();
        public DateTime Created{get;set;} = DateTime.Now;
        public User Creator{get;set;}
        public string ChatGuid{get;set;}

    }
}