using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserDTO:BaseDTO
    {
        public string AvatarUrl { get; set; }
        public string Fullname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedlnLink { get; set; }
        public string YoutubeLink { get; set; }
        //public string Instroduction { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }

        public string RefreshToken { get; set; }
        //public decimal Balance { get; set; } = 0;

        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
