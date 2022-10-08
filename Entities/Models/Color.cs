using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Color : BaseEntity<Guid>
    {
        [Required(ErrorMessage = "Bắt buộc nhập tên màu sắc")]
        public string Name { get; set; }
        [MaxLength(10, ErrorMessage = "Mã hex dài nhất #FFFFFF"), MinLength(4, ErrorMessage = "Mã hex ngắn nhất: #FFF")]
        public string Hex { get; set; }
    }
}
