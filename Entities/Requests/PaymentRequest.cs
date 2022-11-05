using Entities.DTOs;
using System.Collections.Generic;

namespace Entities.Requests
{
    public class PaymentRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public List<CartItem> Carts { get; set; }
    }
}
