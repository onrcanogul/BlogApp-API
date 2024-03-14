using BlogApp.Core.Dtos.Token;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface ITokenService
    {
       TokenDto CreateToken(AppUser user);
    }
}
