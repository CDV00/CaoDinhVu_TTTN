using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ColorDTO: BaseDTO
    {
        public string Name { get; set; }
        public string Hex { get; set; }
    }
}
