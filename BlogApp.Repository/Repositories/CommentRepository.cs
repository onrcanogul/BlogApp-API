using BlogApp.Core.Dtos.Comment;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repository.Repositories
{
    public class CommentRepository : GenericRepository<Comment> , ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetCommentsByUser(string id)
        {
            return await _context.Comments.Where(x => x.UserId ==id).ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByPost(int id)
        {
            return await _context.Comments.Where(x => x.PostId == id).ToListAsync();
        }

        public async Task Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            
        }

        public async Task Update(UpdateCommentDto updateCommentDto)
        {
            var comment = await _context.Comments.FindAsync(updateCommentDto.CommentId);
            comment.Text =  updateCommentDto.Text;
        }
    }
}
