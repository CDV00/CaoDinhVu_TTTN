using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProductColor:BaseEntity<Guid>
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid? ColorId { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
