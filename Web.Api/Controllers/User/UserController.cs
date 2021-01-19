using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Web.Api.Filters;
using Web.Api.Hubs;
using Web.Infrastructure.FileServices;
using Web.Infrastructure.UserServices;
using Web.Models.Entity;
using Web.Models.Models;
using Web.Models.ModelsDTO;

namespace Web.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController:ControllerBase
    {
        private IUserService _context;
        private IMapper _mapper;
        private IWebHostEnvironment _appEnvironment;
        private IFileManager _fileManager;
        private IHubContext<ChatHub> _hubContext;

        public UserController(IUserService context, IMapper mapper, IWebHostEnvironment appEnvironment, IFileManager fileManager, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
            _fileManager = fileManager;
            _hubContext = hubContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getUser()
        {
            return Ok(_mapper.Map<Web.Models.Entity.User, UserDTO>(await _context.GetUser(User.Identity.Name)));
        }
        
        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult> addNewAva([FromForm] NewAvaModel model)
        {
            string path = "/uploads/" + Guid.NewGuid().ToString()+Path.GetExtension(model.file.FileName);
            string pathPhysic= string.Format("{0}{1}",_appEnvironment.WebRootPath,path);
            string pathWeb = string.Format("{0}://{1}{2}", Request.Scheme,Request.Host, path);
            Media media= new Media{PathWeb= pathWeb, PathPhysic= pathPhysic};
            await _context.AddNewAva(media, User.Identity.Name);
            await _fileManager.UploadFile(model.file, pathPhysic);
            await _hubContext.Clients.All.SendAsync(User.Identity.Name+"ChangeAvatar", pathWeb);
            return Ok();
        }
    }
}