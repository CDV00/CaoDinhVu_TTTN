using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Models
{
    public class ProductColor:BaseEntity<Guid>
    {
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}
