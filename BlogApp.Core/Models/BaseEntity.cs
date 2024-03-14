using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
