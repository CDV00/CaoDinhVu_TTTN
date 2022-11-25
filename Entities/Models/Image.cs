using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Image:BaseEntity<Guid>
    {
        public Image() : base() { }
        public string Imglink { get; set; }
        public Guid? ProductColorId { get; set; }
        public virtual ProductColor ProductColor{get;set;}
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
