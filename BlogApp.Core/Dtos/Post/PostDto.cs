using BlogApp.Core.Dtos.Comment;

namespace BlogApp.Core.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public string? UserId { get; set; }
    }
}
