using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Web.Api.Filters;
using Web.Api.Hubs;
using Web.Infrastructure.Auth;
using Web.Infrastructure.ChatServices;
using Web.Infrastructure.UserServices;
using Web.Models.Entity;
using Web.Models.Models;
using Web.Models.ModelsDTO;

namespace Web.Api.Controllers.ChatPM
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private IChatService _service;
        private IAuth _auth;
        private IUserService _user;
        private IMapper _mapper;
        private IHubContext<ChatHub> _hubContext;

        public ChatController(IChatService service, IAuth auth, IUserService user, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _service = service;
            _auth = auth;
            _user = user;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatModel model)
        {
            var user= _auth.GetUser(User.Identity.Name);
            var chat= await _service.CreateChat(new Chat(){Creator=user, Name=model.Name });
            await _service.AddMember(new ChatUser{User= user, Chat= chat});
            chat.CountMembers+=1;
            chat.CountMembersOnline+=1;
            var chatDTO= _mapper.Map<Chat, ChatDTO>(chat);
            var data= new AddToChatModel{Login= user.Login, guid= chatDTO.guid};
            await SendNewChat(data, chatDTO);
            return Ok(_mapper.Map<Chat, ChatDTO>(chat));
        
        }
        [ModelValidation]
        [HttpPost]
        [ChatIsNotNullFilter]
        [UserInChatFilter]
        public async Task<IActionResult> CreateMessage([FromForm] CreateMessageModel model)
        {
            var user= _auth.GetUser(User.Identity.Name);
            var message= new Message(){Content=model.Content, Creator=user, ChatGuid=model.guid};
            message= await _service.AddNewMessage(message, model.guid);
            var messageDTO=  _mapper.Map<Message,MessageDTO>(message);
            await _hubContext.Clients.Group(model.guid).SendAsync(model.guid+"-newMessage",messageDTO);
            return Ok(messageDTO);
        }

        [ModelValidation]
        [HttpPost]
        [UserIsNotNullFilter]
        [ChatIsNotNullFilter]
        public async Task<IActionResult> AddMembers([FromBody] AddToChatModel model)
        {
            if (await _service.IsMember(model.Login, model.guid)) return BadRequest("User is member this chat");
            var user = _auth.GetUser(model.Login);
            await _hubContext.Clients.Group(model.guid).SendAsync(model.guid + "-newMember", _mapper.Map<Web.Models.Entity.User, UserDTO>(user));
            var chat = await _service.GetChatWithAvaByGuid(model.guid);
            await _service.AddMember(new ChatUser { User = user, Chat = chat });
            chat.CountMembers+=1;
            if(user.isOnline){
                chat.CountMembersOnline+=1;
            }
            if(chat.LastMessage!=null)
            {
                chat.Messages.Add(chat.LastMessage);
            }
            var chatDTO = _mapper.Map<Chat, ChatDTO>(chat);
            await SendNewChat(model, chatDTO);
            return Ok();

        }

        [HttpPost]
        [ChatIsNotNullFilter]
        [UserInChatFilter]
        public async Task<IActionResult> LeaveFromChat([FromBody] LeaveFromChatModel model)
        {
            var guid= model.guid;
            var username=User.Identity.Name;
            var chatDTO= _mapper.Map<Chat, ChatDTO>(await _service.GetChatByGuid(guid));
            await _service.LeaveMember(username, guid);
            await _hubContext.Clients.Group(guid).SendAsync(guid+"-leaveMember",_mapper.Map<Web.Models.Entity.User, UserDTO>(_auth.GetUser(username)));
            foreach(var connection in await _user.GetConnections(username))
            {
                await _hubContext.Clients.Client(connection.ConnectionString).SendAsync("leave", chatDTO);
                await _hubContext.Groups.RemoveFromGroupAsync(connection.ConnectionString, guid);
            }
            return Ok();
        }

        [HttpGet]
        [UserInChatFilter]
        public async Task<IActionResult> GetMessages(string guid, int id)
        {
            List<Message> list = await _service.GetMessages(guid, id);
            return Ok(_mapper.Map<List<Message>,List<MessageDTO>>(list));
        }
        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            List<Chat> list = await _service.GetChats(User.Identity.Name);
            foreach(Chat item in list)
            {
                if(item.LastMessage!=null)
                {
                    item.Messages.Add(item.LastMessage);
                }
            }
            return Ok(_mapper.Map<List<Chat>, List<ChatDTO>>(list));        
        }





        private async Task SendNewChat(AddToChatModel model, ChatDTO chatDTO)
        {
            foreach (var connection in await _user.GetConnections(model.Login))
            {
                await _hubContext.Clients.Client(connection.ConnectionString).SendAsync("addChat", chatDTO);
                await _hubContext.Groups.AddToGroupAsync(connection.ConnectionString, model.guid);
            }
        }


    }

}