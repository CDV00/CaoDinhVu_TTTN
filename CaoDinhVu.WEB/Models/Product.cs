using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Models
{
    public class Product : BaseEntity<Guid>
    {
        [Required(ErrorMessage = "Bắt buộc nhập tên Sản phẩm")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập tiêu đề")]
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập giá")]
        [MinLength(0, ErrorMessage = "Giá nhỏ nhất: 0")]
        public decimal Price { get; set; }
        public int Status { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
    }
}
