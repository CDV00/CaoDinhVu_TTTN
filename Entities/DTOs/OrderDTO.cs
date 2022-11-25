using System;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class OrderDTO : BaseDTO
    {
        public decimal TotalPrice { get; set; }

        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? StatusOrder { get; set; } = 1;
        public DateTime? CreateAt { get; set; }
        public UserDTO User { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
    public class OrderInWeekResponse
    {
        public int Amount { get; set; }
        public string Day { get; set; }
    }
    public class OrderByCategoryResponse
    {
        public string CategoryName { get; set; }
        public int Amount { get; set; }
    }
    public class OrderByBrandResponse
    {
        public string BrandName { get; set; }
        public int Amount { get; set; }
    }
}