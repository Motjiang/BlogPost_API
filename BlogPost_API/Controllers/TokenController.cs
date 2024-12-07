using BlogPost_API.Data;
using BlogPost_API.Models.DTO;
using BlogPost_API.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        //get the database context and token service
        private readonly DatabaseContext _context;
        private readonly ITokenService _service;

        //constructor
        public TokenController(DatabaseContext context, ITokenService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshTokenRequest tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _service.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = _context.TokenInfo.SingleOrDefault(u => u.Username == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }
            var newAccessToken = _service.GetToken(principal.Claims);
            var newRefreshToken = _service.GetRefreshToken();
            user.RefreshToken = newRefreshToken;
            _context.SaveChanges();
            return Ok(new RefreshTokenRequest()
            {
                AccessToken = newAccessToken.TokenString,
                RefreshToken = newRefreshToken
            });
        }


        //revoken is use for removing token enntry
        [HttpPost("revoke"), Authorize]
        public IActionResult Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                var user = _context.TokenInfo.SingleOrDefault(u => u.Username == username);
                if (user is null)
                {
                    return BadRequest();
                }

                user.RefreshToken = null;
                _context.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
