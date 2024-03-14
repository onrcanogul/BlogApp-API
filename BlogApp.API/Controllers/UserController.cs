using System.Security.Claims;
using BlogApp.API.Filters;
using BlogApp.Core.Dtos.User;
using BlogApp.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetFromPost(int id)
        {
            
            return ActionResultInstance(await _userService.GetUserFromPost(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetFromComment(int id)
        {
            return ActionResultInstance(await _userService.GetUserFromPost(id));
        }
    }
}
