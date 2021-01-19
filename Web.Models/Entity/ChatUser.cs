namespace Web.Models.Entity
{
    public class ChatUser
    {
        public int Id{get;set;}
        public User User{get;set;}
        public Chat Chat{get;set;}
    }
}