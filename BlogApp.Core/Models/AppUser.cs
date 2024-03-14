using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Core.Models
{
    public class AppUser : IdentityUser
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }

    public class UserRole : IdentityRole
    {
        

    }
}
