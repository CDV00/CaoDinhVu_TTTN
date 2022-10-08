
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserBase : IdentityUser<Guid>
    {
        public UserBase()
        {
            CreateAt = DateTime.UtcNow;
            IsActive = true;
        }

        public DateTime CreateAt { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }


    }
}
