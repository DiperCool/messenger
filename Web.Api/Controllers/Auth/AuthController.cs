using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Web.Api.Filters;
using Web.Infrastructure.Auth;
using Web.Infrastructure.JWT;
using Web.Infrastructure.Validations.ValidationUser;
using Web.Models.Entity;
using Web.Models.Models;

namespace Web.Api.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        IJWT _JWT; 
        IValidationUser _validation;
        IAuth _auth;
        public AuthController(IJWT jwt, IValidationUser validation,IAuth auth)
        {
            _JWT = jwt;
            _validation = validation;
            _auth = auth;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegistarationModel model){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(_validation.userIsExist(model.Login)){
                ModelState.AddModelError("LoginException","Такой логин существует");
                return BadRequest(ModelState);
            }

            User user = new User() {Login = model.Login, Password = model.Password};
            _auth.CreateUser(user);
            return GenerateTokens(user);
        }

        [ModelValidation]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            User user = _auth.GetUser(model.Login, model.Password);
            if (user==null)
            {
                return BadRequest("Login or password is incorrect");
            }
            return GenerateTokens(user);
            
        }
        [ModelValidation]
        [HttpPost]
        public IActionResult Refresh([FromBody] ReturnTokensModel model)
        {
            var principal = _JWT.GetPrincipalFromExpiredToken(model.Token);
            var username = principal.Identity.Name;
            var user=_auth.GetUser(username);
            var savedRefreshToken = user.RefreshToken; //retrieve the refresh token from a data store
            if (savedRefreshToken != model.RefreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newRefreshToken = _JWT.GenerateRefreshToken();
            _auth.SaveRefreshToken(username, newRefreshToken);

            return GenerateTokens(user);
        }
        private IActionResult GenerateTokens(User user)
        {
            var refreshToken = _JWT.GenerateRefreshToken();
            _auth.SetRefreshToken(user.Login, refreshToken);
            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
            };
            string token = _JWT.GenerateToken(claims);
            return Ok(new ReturnTokensModel(token, refreshToken));
        }

    }
    
}