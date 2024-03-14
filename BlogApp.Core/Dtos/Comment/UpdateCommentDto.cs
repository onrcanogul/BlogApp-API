using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Dtos.Comment
{
    public class UpdateCommentDto
    {
        public string UserId { get; set; }

        public int CommentId { get; set; }
        public string Text { get; set; }
    }
}
