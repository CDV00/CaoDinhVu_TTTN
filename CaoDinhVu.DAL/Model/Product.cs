using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.DAL.Model
{
    public class Product : BaseEntiity<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public int Status { get; set; }
    }
}
