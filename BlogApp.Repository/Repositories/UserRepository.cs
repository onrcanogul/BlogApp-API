using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repository.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<AppUser> GetUserFromPost(int postId)
        {
            var user = await _context.Posts.Where(x => x.Id == postId).Select(x => x.User).FirstOrDefaultAsync();
            return user ;
        }
        public async Task<AppUser> GetUserFromComment(int commentId)
        {
            var user = await _context.Comments.Where(x => x.Id == commentId).Select(x => x.Post.User)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
