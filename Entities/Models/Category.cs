using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category : BaseEntity<Guid>
    {
        public Category() { }
        public Category(Guid id, string name, string slug, int status, Guid createBy) :base(id, createBy) 
        {
            Name = name;
            Slug = slug;
            Status = status;
        }

        [Required(ErrorMessage = "Bắt buộc nhập tên danh mục")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public Guid? ParentId { get; set; }
        public int? Orders { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
    }
}
