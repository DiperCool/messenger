using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Infrastructure.ChatServices;
using Web.Models.interfaces;

namespace Web.Api.Filters
{
    public class UserInChatFilter
    {
        
    }
    public class UserInChatFilterAttribute: TypeFilterAttribute
    {
        public UserInChatFilterAttribute():
        base(typeof(UserInChatFilter))
        {
            
        }
        private class UserInChatFilter : Attribute, IAsyncActionFilter
        {
            private IChatService _context;
            public UserInChatFilter(IChatService context)
            {
                _context=context;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if(!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result= new UnauthorizedObjectResult("");
                    return;
                }
                string username= context.HttpContext.User.Identity.Name;
                string guid;
                if(context.HttpContext.Request.Method=="GET")
                {
                    guid= context.HttpContext.Request.Query["guid"];
                }
                else{
                    IChatIsNull model= (IChatIsNull)context.ActionArguments["model"];
                    guid=model.guid;
                }
                if(!await _context.IsMember(username, guid))
                {
                    context.Result= new BadRequestObjectResult("");
                    return;
                }
                await next();
            }
        }
    }
}