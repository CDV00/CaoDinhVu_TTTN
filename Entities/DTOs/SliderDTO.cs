using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class SliderDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Position { get; set; }
        //[Required]
        public string Img { get; set; }
        public string Attribute { get; set; }
        public string Sort_order { get; set; }
        public int Orders { get; set; }
        public int Status { get; set; }
    }
}
