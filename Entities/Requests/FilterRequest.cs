using Entities.Models;
using System;

namespace Entities.Requests
{
    public class FilterRequest
    {
        public Guid BrandId { get; set; } = Guid.Empty;
        public Guid CategoryId { get; set; } = Guid.Empty;
        public Guid ColorId { get; set; } = Guid.Empty;
        public Guid OptionId { get; set; } = Guid.Empty;
        public decimal PriceMin { get; set; } = 0;
        public decimal PriceMax { get; set; } = 0;
        public string KeyWork { get; set; } = null;
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
