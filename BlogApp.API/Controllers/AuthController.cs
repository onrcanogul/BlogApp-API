using BlogApp.Core.Dtos.Token;
using BlogApp.Core.Dtos.User;
using BlogApp.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto) //-->Login
        {
            return ActionResultInstance(await _authenticationService.CreateTokenAsync(loginDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return ActionResultInstance(await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto));
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return ActionResultInstance(await _authenticationService.RevokeRefreshToken(refreshTokenDto));
        }
    }
}
