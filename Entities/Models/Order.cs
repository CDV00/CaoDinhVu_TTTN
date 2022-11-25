using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order : BaseEntity<Guid>
    {
        public Order() : base() { }
        public decimal? TotalPrice { get; set; }

        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? StatusOrder { get; set; } = 1;
        
        public AppUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
