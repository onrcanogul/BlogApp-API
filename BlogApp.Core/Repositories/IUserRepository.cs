using BlogApp.Core.Models;

namespace BlogApp.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<AppUser> GetUserFromPost(int postId);


        Task<AppUser> GetUserFromComment(int commentId);
    }
}
