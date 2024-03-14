using BlogApp.Core.Dtos.Comment;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Models;

namespace BlogApp.Core.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByUser(string id);
        Task<List<Comment>> GetCommentsByPost(int id);
        Task Delete(int id);
        Task Update(UpdateCommentDto updateCommentDto);
    }
}
