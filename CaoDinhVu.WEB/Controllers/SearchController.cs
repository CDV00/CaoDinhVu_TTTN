using CaoDinhVu.BLL.Services;
using Entities.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Entities.Responses;
using Entities.DTOs;
using Entities.Constants;

namespace CaoDinhVu.WEB.Controllers
{
    public class SearchController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductSevice _productSevice;
        private readonly IBrandService _brandService;
        private readonly ISliderService _sliderService;
        private readonly IUploadImage _uploadImage;


        public SearchController(ICategoryService categoryService,
                              IProductSevice productSevice,
                              IUploadImage uploadImage,
                              IBrandService brandService,
                              ISliderService sliderService)
        {
            _categoryService = categoryService;
            _productSevice = productSevice;
            _uploadImage = uploadImage;
            _brandService = brandService;
            _sliderService = sliderService;
        }
        [HttpGet]
        public async Task<IActionResult> index(string keyWork, int? page)
        {
            
            if (page == null)
                page = 1;
            int pageSize = 8;
            Search.KeyWork = keyWork;
            ViewBag.keywork = keyWork;
            ViewBag.page = page;
            var products = await _productSevice.GetByKeyword(new PagingRequest(page, pageSize), keyWork);
            return View(products);
        }

        public async Task<IActionResult> _Filter()
        {

            /*if (page == null)
                page = 1;
            int pageSize = 2;
            ViewBag.keywork = Search.KeyWork;
            ViewBag.page = page;*/

            var filterRequest = new FilterRequest()
            {
                BrandId = FilterRequestConstan.Brand.Value,
                CategoryId = FilterRequestConstan.Category.Value,
                ColorId = FilterRequestConstan.Color.Value,
                OptionId = FilterRequestConstan.Option.Value,
                PriceMin = FilterRequestConstan.PriceMin.Value,
                PriceMax = FilterRequestConstan.PriceMax.Value,
                Page = FilterRequestConstan.Page,
                PageSize = FilterRequestConstan.PageSize,
                KeyWork = FilterRequestConstan.KeyWork
            };
            var products = await _productSevice.Filter(filterRequest);

            return PartialView("_listProduct", products);
            //return View(products);
        }

        [HttpPost]
        public JsonResult Filter(Guid? brand, Guid? category, Guid? color, Guid? option, decimal? priceMin, decimal? priceMax, int? page)
        {

            FilterRequestConstan.Brand = Guid.Empty;
            FilterRequestConstan.Category = Guid.Empty;
            FilterRequestConstan.Color = Guid.Empty;
            FilterRequestConstan.Option = Guid.Empty;
            FilterRequestConstan.PriceMin = 0;
            FilterRequestConstan.PriceMax = 0;
            FilterRequestConstan.Page = 1;
            FilterRequestConstan.PageSize = 12;
            FilterRequestConstan.KeyWork = null;
            if (page == null)
                page = 1;
            int pageSize = 8;
            ViewBag.keywork = Search.KeyWork;
            ViewBag.page = page;
            if(brand != null)
            {
                FilterRequestConstan.Brand = brand;
            }
            if (category != null)
            {
                FilterRequestConstan.Category = category;
            }
            if (color != null)
            {
                FilterRequestConstan.Color = color;
            }
            if (option != null)
            {
                FilterRequestConstan.Option = option;
            }
            if (priceMin != null)
            {
                FilterRequestConstan.PriceMin = priceMin;
            }
            if (priceMax != null)
            {
                FilterRequestConstan.PriceMax = priceMax;
            }
            if (page != null)
            {
                FilterRequestConstan.Page = page;
            }
            if (pageSize != null)
            {
                FilterRequestConstan.PageSize = pageSize;
            }
            if (Search.KeyWork != null)
            {
                FilterRequestConstan.KeyWork = Search.KeyWork;
            }

            
            
            
            
            
            
            
            

            return Json(new
            {
                status = true,

            });


            //var products = await _productSevice.Filter(filterRequest);

            //return PartialView("_listProduct", products);
            //return View(products);
        }


        public IActionResult SearchPagging(string keyWork, int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 8;
            var products = _productSevice.GetByKeyword(new PagingRequest(page, pageSize), keyWork);
            return PartialView("_listProduct", products);
        }
        [HttpPost]
        public IActionResult SearchByKeyWorkk(string keyWork, int? page)
        {
            if(keyWork == null)
                keyWork = "a";
            if (page == null)
                page = 1;
            int pageSize = 10;
            //PagingResponse<ListProductDTO> products = new PagingResponse<ListProductDTO>();
            //List<BrandDTO> brands = new List<BrandDTO>();
            var products = _productSevice.GetByKeyword(new PagingRequest(page, pageSize), keyWork);
            //brands = await _brandService.getAll();

            return PartialView("_listProduct", products);
            //Query.parseJSON(products);
            //return Json(JsonConvert.SerializeObject(products));
            /*return Json(new { 
                    status = true,
                    data = products
            });*/
        }

    }
}