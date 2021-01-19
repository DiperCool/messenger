namespace Web.Models.ModelsDTO
{
    public class UserDTO
    {
        public int Id{get;set;}
        public string Login{get;set;}
         public bool isOnline{get;set;}
        public MediaDTO CurrentAva { get; set; }= new MediaDTO();
    }
}