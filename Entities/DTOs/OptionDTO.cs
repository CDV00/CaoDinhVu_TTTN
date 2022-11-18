using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OptionDTO : BaseDTO
    {
        public int RAM { get; set; }
        public int ROM { get; set; }
    }
}