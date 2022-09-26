using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Models
{
    public partial class BaseEntity<T>
    {
        public BaseEntity()
        {
            CreateAt = DateTime.UtcNow;
        }
        public BaseEntity(T id)
        {
            Id = id;
            CreateAt = DateTime.UtcNow;
            IsActive = true;
            IsDelete = false;
        }
        [Key]
        public T Id { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public Guid CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
