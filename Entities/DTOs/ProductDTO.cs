using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDTO: BaseDTO
    {
        public string Name { get; set; }
        public string Thumbnails { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ImageDTO> Images { get; set; }
        public Guid CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; }
        public Guid BrandId { get; set; }
        public virtual BrandDTO Brand { get; set; }
        public string Description { get; set; }
        public Guid? DetailId { get; set; }
        //public virtual DetailDTO Detail { get; set; }
        public virtual DetailDTO Detail { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }

        public string Screen { get; set; }
        public string Camera { get; set; }
        public string OperatingSystem { get; set; }
        public string CPU { get; set; }
        public string ROM { get; set; }
        public string RAM { get; set; }
        public string Connection { get; set; }
        public string Battery { get; set; }
        public string Charger { get; set; }
        public string GeneralInformation { get; set; }
        public List<IFormFile> Files { get; set; }

        public virtual ICollection<ProductColorDTO> ProductColors { get; set; }
        public virtual ICollection<ProductOptionDTO> ProductOptions { get; set; }
    }
}
