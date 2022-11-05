using Entities.Models;
using System;

namespace Entities.Requests
{
    public class FilterRequest
    {
        public Guid? Brand { get; set; }
        public Guid? Category { get; set; }
        public Guid? Color { get; set; }
        public Guid? Option { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public string KeyWork { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
