using System.Collections.Generic;
using System.Security.Claims;

namespace Web.Infrastructure.JWT
{
    public interface IJWT
    {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}