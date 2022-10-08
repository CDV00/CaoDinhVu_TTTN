using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Image:BaseEntity<Guid>
    {
        public string Imglink { get; set; }
        public virtual ProductColor ProductColor{get;set;}
        public virtual Product Product { get; set; }
    }
}
