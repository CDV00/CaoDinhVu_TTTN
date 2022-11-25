using System;

namespace Entities.DTOs
{
    public class OrderDetailDTO : BaseDTO
    {
        public int? Amount { get; set; }
        public Guid? OrderId { get; set; }
        public ProductDTO Product { get; set; }
        public Guid? ProductId { get; set; }
        public OrderDTO Order { get; set; }
        public ProductColorDTO ProductColor { get; set; }
        public Guid? ProductColorId { get; set; }
        public Guid? ProductOptionId { get; set; }
        public ProductOptionDTO ProductOption { get; set; }
        public int? Status { get; set; }
    }
}