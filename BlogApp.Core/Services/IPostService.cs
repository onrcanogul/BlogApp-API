using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Dtos.Post;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface IPostService : IService<Post,PostDto>
    {
        Task<CustomResponseDto<List<PostDto>>> GetPostByUser(GetByIdDto user);
        Task<CustomResponseDto<IEnumerable<PostDto>>> GetAll();
        Task<CustomResponseDto<PostDto>> GetPostById(int id);
        Task<CustomResponseDto<NoContentDto>> Update(UpdatePostDto updatePostDto, int id);
        Task<CustomResponseDto<NoContentDto>> Delete(int id);
    }
}
