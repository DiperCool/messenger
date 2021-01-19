using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Infrastructure.ChatServices;
using Web.Models.interfaces;

namespace Web.Api.Filters
{     public class ChatIsNotNullFilterAttribute: TypeFilterAttribute
    {
        public ChatIsNotNullFilterAttribute():
        base(typeof(ChatIsNotNullFilter))
        {
            
        }
        private class  ChatIsNotNullFilter : Attribute, IAsyncActionFilter
        {
            private IChatService _context;
            public  ChatIsNotNullFilter(IChatService context)
            {
                _context=context;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                IChatIsNull model= (IChatIsNull)context.ActionArguments["model"];
                var guid=model.guid;
                if(!await _context.ChatIsExist(guid))
                {
                    context.Result= new UnauthorizedObjectResult("");
                    return;
                }
                await next();
            }
        }
    }
}