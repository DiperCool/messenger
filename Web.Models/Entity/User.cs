using System;
using System.Collections.Generic;

namespace Web.Models.Entity
{
    public class User
    {
        public int Id{get;set;}
        public string Login{get;set;}
        public bool isOnline{get;set;}= false;
        public string Password{get;set;}
        public string RefreshToken{get;set;}
        public List<Connection> Connetions= new List<Connection>();
        public List<Media> Avas {get;set;} = new List<Media>();
        public Media CurrentAva { get; set; }
        public DateTime CreatedTime{get;set;}= DateTime.Now;
    }
}