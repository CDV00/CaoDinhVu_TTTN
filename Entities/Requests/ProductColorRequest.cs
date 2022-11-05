using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Requests
{
    public class ProductColorRequest
    {
        public Guid? Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
    }
}
