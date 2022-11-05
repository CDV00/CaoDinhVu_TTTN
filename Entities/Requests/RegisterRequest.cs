using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests
{
    public class RegisterRequest
    {

        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Role { get; set; } = "Custommer";

    }
}
