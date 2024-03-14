using BlogApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}
