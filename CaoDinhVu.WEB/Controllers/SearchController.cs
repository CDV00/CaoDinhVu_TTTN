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
            int pageSize = 2;
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
                Brand = FilterRequestConstan.Brand,
                Category = FilterRequestConstan.Category,
                Color = FilterRequestConstan.Color,
                Option = FilterRequestConstan.Option,
                PriceMin = FilterRequestConstan.PriceMin,
                PriceMax = FilterRequestConstan.PriceMax,
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

            FilterRequestConstan.Brand = null;
            FilterRequestConstan.Category = null;
            FilterRequestConstan.Color = null;
            FilterRequestConstan.Option = null;
            FilterRequestConstan.PriceMin = null;
            FilterRequestConstan.PriceMax = null;
            FilterRequestConstan.Page = null;
            FilterRequestConstan.PageSize = null;
            FilterRequestConstan.KeyWork = null;
            if (page == null)
                page = 1;
            int pageSize = 2;
            ViewBag.keywork = Search.KeyWork;
            ViewBag.page = page;

            FilterRequestConstan.Brand = brand;
            FilterRequestConstan.Category = category;
            FilterRequestConstan.Color = color;
            FilterRequestConstan.Option = option;
            FilterRequestConstan.PriceMin = priceMin;
            FilterRequestConstan.PriceMax = priceMax;
            FilterRequestConstan.Page = page;
            FilterRequestConstan.PageSize = pageSize;
            FilterRequestConstan.KeyWork = Search.KeyWork;

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
            int pageSize = 10;
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