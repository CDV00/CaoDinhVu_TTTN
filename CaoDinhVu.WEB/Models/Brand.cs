using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Models
{
    public class Brand : BaseEntity<Guid>
    {
        [Required(ErrorMessage = "Bắt buộc nhập tên thương hiệu")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public Guid ParentId { get; set; }
        public int? Orders { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
    }
}
