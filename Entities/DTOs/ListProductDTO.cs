﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ListProductDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Thumbnails { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; }
        public Guid BrandId { get; set; }
        public virtual BrandDTO Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
    }
}
