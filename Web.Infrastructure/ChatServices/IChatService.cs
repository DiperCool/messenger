using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Entity;

namespace Web.Infrastructure.ChatServices
{
    public interface IChatService
    {
        Task<Chat> CreateChat(Chat chat);
        Task<List<Chat>> GetChats(string login);
        Task AddMember(ChatUser login);
        Task LeaveMember(string login, string guid);
        Task<Message> AddNewMessage(Message message,string guid);
        Task<Chat> GetChatByGuid(string guid);
        Task<List<Message>> GetMessages(string guid, int id);
        Task<bool> IsMember(string login, string guid);
        Task<Chat> GetChatWithAvaByGuid(string guid);
        Task<bool> ChatIsExist(string guid);
    }
}