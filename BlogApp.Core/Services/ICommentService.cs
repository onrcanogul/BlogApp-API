using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Comment;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface ICommentService : IService<Comment,CommentDto>
    {
        Task<CustomResponseDto<List<CommentDto>>> GetCommentsByUser(string id);
        Task<CustomResponseDto<List<CommentDto>>> GetCommentsByPost(int id);
        Task<CustomResponseDto<NoContentDto>> Delete(int id);
        Task<CustomResponseDto<NoContentDto>> Update(UpdateCommentDto updateCommentDto);
    }
}
