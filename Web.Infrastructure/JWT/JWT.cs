using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Web.Infrastructure.Auth;
using Web.Models.Config;

namespace Web.Infrastructure.JWT
{
    public class JWT: IJWT
    {
        private IAuth _auth;

        public JWT(IAuth auth)
        {
            _auth = auth;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(issuer: JWTconfig.ISSUER,
                audience: JWTconfig.AUDIENCE,
                claims: claims, //the user's claims, for example new Claim[] { new Claim(ClaimTypes.Name, "The username"), //... 
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(JWTconfig.LIFETIME),
                signingCredentials: new SigningCredentials(JWTconfig.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );    

            return new JwtSecurityTokenHandler().WriteToken(jwt); //the method is called WriteToken but returns a string

        }
        
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()){
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);;
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JWTconfig.GetSymmetricSecurityKey(),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}