using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OptionDTO : BaseDTO
    {
        public int RAM { get; set; }
        public int ROM { get; set; }
    }
    public class OrderDetailDTO : BaseDTO
    {
        public int? Amount { get; set; }
        public Guid? OrderId { get; set; }
        public ProductDTO Product { get; set; }
        public OrderDTO Order { get; set; }
        public ProductColorDTO ProductColor { get; set; }
        public ProductOptionDTO productOption { get; set; }
        public int? Status { get; set; }
    }
    public class OrderDTO : BaseDTO
    {
        public decimal TotalPrice { get; set; }

        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? StatusOrder { get; set; } = 1;

        public UserDTO User { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}