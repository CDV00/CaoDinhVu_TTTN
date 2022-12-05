using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Requests
{
    public class pproductColorRequest
    {
        public string ProductId { get; set; }
        public string ColorId { get; set; }
    }
    public class ProductColorRequest
    {
        public Guid? Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
    }
}
