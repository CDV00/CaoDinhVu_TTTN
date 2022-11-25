using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Entities.Requests
{
    public class PaymentRequest
    {
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public Guid? CreateBy { get; set; } = Guid.Empty;
        public decimal TotalPrice { get; set; } = 0;
        public List<CartItem> Carts { get; set; }
    }
}
