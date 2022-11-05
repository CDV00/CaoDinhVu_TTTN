using System;

namespace Entities.Requests
{
    public class ProductOptionRequest
    {
        public Guid? Id { get; set; }
        public int? Number { get; set; }
        public Guid? ProductColorId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? OptionId { get; set; }
        public decimal Price { get; set; }
    }
}
