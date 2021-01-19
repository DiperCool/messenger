using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.Entity
{
    public class Chat
    {
        public int Id{get;set;}
        public string Name { get; set; }
        public string guid{get;set;} = Guid.NewGuid().ToString();
        public DateTime Created{get;set;} = DateTime.Now;
        public User Creator{get;set;}
        public List<Message> Messages{get;set;} = new List<Message>();
        public Message LastMessage{get;set;}=null;
        public DateTime LastUpdate{get;set;}=DateTime.Now;
        public List<Media> Avas{get;set;} = new List<Media>();
        public Media СurrentAva{get;set;}
        [NotMapped]
        public int CountMembers{get;set;}
        [NotMapped]
        public int CountMembersOnline{get;set;}
    }
}