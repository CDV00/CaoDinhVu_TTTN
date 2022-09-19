using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.DAL.Model
{
    public class Order : BaseEntiity<Guid>
    {
        public ICollection<ProductDetail> productDetails { get; set; }
        //public 
    }
}
