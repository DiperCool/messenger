using AutoMapper;
using Web.Models.Entity;
using Web.Models.ModelsDTO;

namespace Web.Api.Mapper
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Media, MediaDTO>();
            CreateMap<Message, MessageDTO>();
            CreateMap<Chat, ChatDTO>();
        }
    }
}