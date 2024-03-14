using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

//using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repository.Repositories
{
    public class PostRepository : GenericRepository<Post> , IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<List<Post>> GetPostByUser(GetByIdDto user)
        {
            return await _context.Posts.Include(x => x.User).Where(x => x.User.Id == user.Id).ToListAsync();
        }

        public new async Task<IEnumerable<Post>> GetAllAsync()
        {
            var result = await _context.Posts.Include(x => x.Comments).ToListAsync();
            return result;
        }

        public new async Task<Post> GetById(int id)
        {
            var result = await _context.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
        }
    }
}
