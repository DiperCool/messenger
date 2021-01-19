using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Web.Infrastructure.ChatServices;
using Web.Infrastructure.UserServices;
using Web.Models.Entity;
using Web.Models.ModelsDTO;

namespace Web.Api.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {
        private IChatService _service;
        private IUserService _users;
        private ILogger<ChatHub> _logger;

        public ChatHub(IChatService service, IUserService users, ILogger<ChatHub> logger)
        {
            _service = service;
            _users = users;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {   
            string username= Context.User.Identity.Name;
            await _users.setUserStatus(username, true);
            await _users.AddConnection(Context.ConnectionId, username);
            foreach(var item in await _service.GetChats(username))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, item.guid);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string username= Context.User.Identity.Name;
            await _users.RemoveConnection(Context.ConnectionId);
            if(await _users.CountConnectionsUser(username)==0)
            {
               await _users.setUserStatus(username, false);
            }
            if(exception!=null)
            {
                _logger.LogError(exception.Message);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessege(Chat chat, MessageDTO message)
        {
            await Clients.Group(chat.guid).SendAsync("ReceiveMessege", chat, message);
        }
    }
}