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
        public ProductsByCategory ProductsByCategory = new ProductsByCategory();
        public listProductsByBrabd ProductsByBrabds =new listProductsByBrabd();
    }
    public class ProductsByCategory
    {
        public ProductsByCategory() {}
        public CategoryDTO category { get; set; } = null;
        public PagingResponse<ListProductDTO> products { get; set; } = null;
    }
    public class listProductsByBrabd
    {
        //public listProductsByBrabd() { }
        public BrandDTO brand { get; set; } = null;
        public PagingResponse<ListProductDTO> products { get; set; } = null;
    }
    public class MenuDTO:BaseDTO
    {
        public List<CategoryDTO> listCategory { get; set; } 
        public List<BrandDTO> listBrands { get; set; }

    }
    public class DashboardDTO
    {
        public List<OrderInWeekResponse> OrderInWeek { get; set; }
        public List<OrderByCategoryResponse> OrderByCategory { get; set; }
        public List<OrderByBrandResponse> OrderByBrand { get; set; }
    }

}
