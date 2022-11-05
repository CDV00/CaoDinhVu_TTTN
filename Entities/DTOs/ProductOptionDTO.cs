using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductOptionDTO: BaseDTO
    {
        public int Number { get; set; } = 0;
        public virtual ProductColorDTO ProductColor { get; set; }
        public virtual ProductDTO Product { get; set; }
        public virtual OptionDTO Option { get; set; }
        public decimal Price { get; set; }
    }
}
