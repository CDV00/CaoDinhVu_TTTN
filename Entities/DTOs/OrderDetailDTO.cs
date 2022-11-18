using System;

namespace Entities.DTOs
{
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
}