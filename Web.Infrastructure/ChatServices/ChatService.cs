using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models.Db;
using Web.Models.Entity;
namespace Web.Infrastructure.ChatServices
{
    public class ChatService:IChatService
    {
        private Context _context;

        public ChatService(Context context)
        {
            _context = context;
        }

        public async Task<Chat> CreateChat(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<List<Chat>> GetChats(string login)
        {
            var chats= await _context.ChatsUsers
                .AsNoTracking()
                .Where(x=>x.User.Login==login)
                .Include(x=>x.User)
                .Include(x=>x.Chat)
                    .ThenInclude(x=>x.LastMessage)
                        .ThenInclude(x=>x.Creator)
                            .ThenInclude(x=>x.CurrentAva)
                .Include(x=>x.Chat)
                    .ThenInclude(x=>x.СurrentAva)
                .OrderByDescending(x=>x.Chat.LastUpdate)
                .Select(x=>x.Chat)
                .ToListAsync();
            chats.ForEach(delegate (Chat x){
                x.CountMembers= _context.ChatsUsers.Where(y=>y.Chat.guid==x.guid).Count();
                x.CountMembersOnline=  _context.ChatsUsers.Where(y=>y.Chat.guid==x.guid&&y.User.isOnline).Count();
            });
            return chats;
        }

        public async Task AddMember(ChatUser re)
        {
            re.Chat.LastUpdate=DateTime.Now;
            _context.Chats.Update(re.Chat);
            await _context.ChatsUsers.AddAsync(re);
            await _context.SaveChangesAsync();
        }
        public async Task<Message> AddNewMessage(Message message,string guid)
        {
            Chat chat=await GetChatByGuid(guid);
            chat.Messages.Add(message);
            chat.LastMessage=message;
            chat.LastUpdate=DateTime.Now;
            _context.Chats.Update(chat);
            await _context.SaveChangesAsync();
            return message;
        }


        public async Task<Chat> GetChatByGuid(string guid)
        {
            return await _context.Chats
                            .FirstOrDefaultAsync(x=>x.guid==guid);
        }
        public async Task<Chat> GetChatWithAvaByGuid(string guid)
        {
            var chat= await _context.Chats
                            .AsNoTracking()
                            .Include(x=>x.LastMessage)
                                .ThenInclude(x=>x.Creator)
                                    .ThenInclude(x=>x.CurrentAva)
                            .Include(x=>x.СurrentAva)
                            .FirstOrDefaultAsync(x=>x.guid==guid);
            chat.CountMembers= _context.ChatsUsers.Where(y=>y.Chat.guid==guid).Count();
            chat.CountMembersOnline=  _context.ChatsUsers.Where(y=>y.Chat.guid==guid&&y.User.isOnline).Count();
            return chat;
        }
        public async Task<List<Message>> GetMessages(string guid,int id)
        {
            return (await _context.Messages
                        .AsNoTracking()
                        .OrderByDescending(x=>x.Id)
                        .Where(x=>x.ChatGuid==guid&&x.Id<id)
                        .Include(x=>x.Creator)
                            .ThenInclude(x=>x.CurrentAva)
                        .Take(20)
                        .ToListAsync()).OrderBy(x=>x.Id).ToList();

        }
        public async Task<bool> IsMember(string login, string guid)
        {
            return await _context.ChatsUsers.AnyAsync(x=>x.User.Login==login&&x.Chat.guid==guid);
        }

        public async Task LeaveMember(string login, string guid)
        {
            var ChatsUsers= await _context.ChatsUsers
                                .FirstOrDefaultAsync(x=>x.Chat.guid==guid&&x.User.Login==login);
            _context.ChatsUsers.Remove(ChatsUsers);
            _context.SaveChanges();
        }

        public async Task<bool> ChatIsExist(string guid)
        {
            return await _context.Chats.AsNoTracking().AnyAsync(x=>x.guid==guid);
        }
    }
}