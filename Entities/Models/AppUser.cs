using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AppUser : UserBase
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

        public string Address { get; set; }
        public string Gender { get; set; }
        public string RefreshToken { get; set; }
        //public decimal Balance { get; set; } = 0;

        public DateTime? RefreshTokenExpiryTime { get; set; }

        //public Guid? CategoryId { get; set; }
        //public Category Category { get; set; }

        //public ICollection<Enrollment> Enrollments { get; set; }
        //public ICollection<CourseCompletion> CourseCompletions { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        //public ICollection<ShoppingCart> Carts { get; set; }
    }
}
