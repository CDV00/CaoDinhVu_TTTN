using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoDinhVu.DAL.Model
{
    public partial class BaseEntiity<T>
    {
        public BaseEntiity()
        {
            CreateAt = DateTime.UtcNow;
        }
        public BaseEntiity(T id)
        {
            Id = id;
            CreateAt = DateTime.UtcNow;
            IsActive = true;
            IsDelete = false;
        }
        [Key]
        public T Id { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
