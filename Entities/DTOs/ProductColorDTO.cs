using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductColorDTO:BaseDTO
    {
        public virtual ProductDTO Product { get; set; }
        public virtual ColorDTO Color { get; set; }
        public virtual ICollection<ProductOptionDTO> ProductOptions { get; set; }
        public virtual ICollection<ImageDTO> Images { get; set; }
    }
}
