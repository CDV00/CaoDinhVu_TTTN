using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProductOption: BaseEntity<Guid>
    {
        public ProductOption() : base() { }
        public int? Number { get; set; } = 0;
        public Guid? ProductColorId { get; set; }
        public virtual ProductColor ProductColor { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid OptionId { get; set; }
        public virtual Option Option { get; set; }
        public int? Status { get; set; } = 1;
        //[Required(ErrorMessage = "Bắt buộc nhập giá")]
        //[MinLength(0, ErrorMessage = "Giá nhỏ nhất: 0")]
        public decimal? Price { get; set; } = 100000;
    }
}
