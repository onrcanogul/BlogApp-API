using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.User;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface IUserService : IService<AppUser,AppUserDto>
    {

        Task<CustomResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<CustomResponseDto<AppUserDto>> GetUserFromPost(int postId);
        Task<CustomResponseDto<AppUserDto>> GetUserFromComment(int commentId);
    }
}
