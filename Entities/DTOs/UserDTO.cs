using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserDTO:BaseDTO
    {
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedlnLink { get; set; }
        public string YoutubeLink { get; set; }
        //public string Instroduction { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }

        public int Role { get; set; } = 3;

        public string Address { get; set; }
        public string Gender { get; set; }
        public string RefreshToken { get; set; }
        public int? Status { get; set; }
        //public decimal Balance { get; set; } = 0;

        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
    public class ProfileAddress
    {
        public UserDTO AppUser { get; set; }
        public String tinh { get; set; }
        public String huyen { get; set; }
        public String phuong { get; set; }
        public String chiTiet { get; set; }
    }
    public class ProfileMain
    {
        public UserDTO AppUser { get; set; }
        public IEnumerable<OrderDTO> Order { get; set; }
        public int AmountOrder { get; set; }
        public int AmountWaitForConfirmation { get; set; }
        public int AmountAwaitingDelivery { get; set; }
        public int AmountDeliveredItems { get; set; }

    }
    public class ProfileSetting
    {
        public Guid? Id { get; set; } = Guid.Empty;
        public UserDTO AppUser { get; set; }
        public string SpecificAddress { get; set; }
        public string wards { get; set; }
        public string District  { get; set; }
        public string Province { get; set; }


    }
    public class ChangePasswordRequest
    {
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
