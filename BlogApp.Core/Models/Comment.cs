using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Models
{
    public class Comment : BaseEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; }
        public string? Text { get; set; }
    }
}
