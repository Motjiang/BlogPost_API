using BlogPost_API.Models.DTO;
using System.Security.Claims;

namespace BlogPost_API.Services.Abstract
{
    public interface ITokenService
    {
        TokenResponse GetToken(IEnumerable<Claim> claim);

        string GetRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
