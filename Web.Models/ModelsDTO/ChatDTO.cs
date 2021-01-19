using System;
using System.Collections.Generic;
using Web.Models.Entity;

namespace Web.Models.ModelsDTO
{
    public class ChatDTO
    {
        public string Name{get;set;}
        public string guid{get;set;}
        public DateTime Created{get;set;}
        public MessageDTO LastMessage{get;set;}
        public List<MessageDTO> Messages{get;set;}= new List<MessageDTO>();
        public DateTime LastUpdate{get;set;}
        public Media СurrentAva{get;set;}
        public int CountMembers{get;set;}
        public int CountMembersOnline{get;set;}
    }
}