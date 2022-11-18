using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class BaseEntity<T>
    {
        public BaseEntity()
        {
            CreateAt = DateTime.UtcNow;
        }
        public BaseEntity(T id, Guid createBy)
        {
            Id = id;
            CreateAt = DateTime.UtcNow;
            IsActive = true;
            IsDelete = false;
            CreateBy = createBy;
        }
        [Key]
        public T Id { get; set; }
        //[Required]
        public DateTime? CreateAt { get; set; }
        //[Required]
        public Guid? CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid? UpdateBy { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsDelete { get; set; } = false;
    }
}
