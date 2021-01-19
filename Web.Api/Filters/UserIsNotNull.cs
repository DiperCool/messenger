using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Infrastructure.ChatServices;
using Web.Infrastructure.UserServices;
using Web.Models.interfaces;

namespace Web.Api.Filters
{
    public class UserIsNotNullFilterAttribute: TypeFilterAttribute
    {
        public UserIsNotNullFilterAttribute():
        base(typeof(UserIsNotNullFilter))
        {
            
        }
        private class UserIsNotNullFilter : Attribute, IAsyncActionFilter
        {
            private IUserService _context;
            public UserIsNotNullFilter(IUserService context)
            {
                _context=context;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                IUserIsNull model= (IUserIsNull)context.ActionArguments["model"];
                var login=model.Login;
                if(!await _context.UserIsExist(login))
                {
                    context.Result= new UnauthorizedObjectResult("");
                    return;
                }
                await next();
            }
        }
    }
}