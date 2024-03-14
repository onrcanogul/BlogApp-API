using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Models
{
    public class UserRefreshToken : BaseEntity
    {
        public string UserId  { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
