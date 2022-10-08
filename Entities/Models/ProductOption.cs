using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProductOption: BaseEntity<Guid>
    {
        
        public int Number { get; set; } = 0;
        public virtual ProductColor ProductColor { get; set; }
        public virtual Product Product { get; set; }
        public virtual Option Option { get; set; }
    }
}
