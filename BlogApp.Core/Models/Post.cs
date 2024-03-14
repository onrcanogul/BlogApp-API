using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
