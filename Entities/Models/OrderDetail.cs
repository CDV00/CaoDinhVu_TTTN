using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OrderDetail : BaseEntity<Guid>
    {
        public OrderDetail() : base() { }
        public int? Amount { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public Guid? ProductColorId { get; set; }
        public ProductColor ProductColor { get; set; }
        public Guid? productOptionId { get; set; }
        public ProductOption productOption { get; set; }
        public int? Status { get; set; }
    }
}
