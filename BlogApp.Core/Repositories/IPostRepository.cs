using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Models;

namespace BlogApp.Core.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
       Task< List<Post>> GetPostByUser(GetByIdDto user);
       Task<IEnumerable<Post>> GetAllAsync();
       Task<Post> GetById(int id);
       Task Delete(int id);
    }
}
