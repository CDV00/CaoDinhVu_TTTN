using System;

namespace Entities.Requests
{
    public class ProductRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Thumbnails { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Status { get; set; }
    }
}
