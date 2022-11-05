using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class FilterDTO : BaseDTO
    {
        public List<CategoryDTO> listCategory { get; set; }
        public List<BrandDTO> listBrands { get; set; }
        public List<OptionDTO> listOptions { get; set; }
        public List<ColorDTO> listColors { get; set; }

    }
}
