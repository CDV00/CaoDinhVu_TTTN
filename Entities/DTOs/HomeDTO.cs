using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class HomeDTO:BaseDTO
    {
        public List<CategoryDTO> listCategory { get; set; }
        public List<SliderDTO> listSlider { get; set; }
        public List<BrandDTO> listBrands { get; set; }
        public PagingResponse<ListProductDTO> listProducts { get; set; }
        public PagingResponse<ListProductDTO> listProductsByCategory { get; set; }
        public PagingResponse<ListProductDTO> listProductsByBrabd { get; set; }

    }
    public class MenuDTO:BaseDTO
    {
        public List<CategoryDTO> listCategory { get; set; }
        public List<BrandDTO> listBrands { get; set; }

    }

}
