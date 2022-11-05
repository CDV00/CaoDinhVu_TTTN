using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class BrandDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public Guid? ParentId { get; set; }
        public int? Orders { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
    }
}
