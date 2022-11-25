using CaoDinhVu.BLL.Services;
using CaoDinhVu.WEB.Models;
using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductSevice _productSevice;
        private readonly IBrandService _brandService;
        private readonly ISliderService _sliderService;
        private readonly IUploadImage _uploadImage;
        
        
        public HomeController(ILogger<HomeController> logger,
                              ICategoryService categoryService,
                              IProductSevice productSevice,
                              IUploadImage uploadImage,
                              IBrandService brandService,
                              ISliderService sliderService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productSevice = productSevice;
            _uploadImage = uploadImage;
            _brandService = brandService;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Response.Cookies.Append("user_id", "1");
            HomeDTO homeDTO = new HomeDTO();
            homeDTO.listCategory = await _categoryService.getAll();
            
            homeDTO.listBrands = await _brandService.getAll();
            homeDTO.listSlider = await _sliderService.getAll();
            homeDTO.listProducts = await _productSevice.GetAllNoTracking(new PagingRequest(1,100));
            Guid categoryId = (homeDTO.listCategory.FirstOrDefault()).Id.Value;
            homeDTO.ProductsByCategory.category = await _categoryService.getById(categoryId);
            /*if(category != null)
            {
                homeDTO.ProductsByCategory.category = new CategoryDTO();
                homeDTO.ProductsByCategory.category = category;
            }*/
            
            homeDTO.ProductsByCategory.products = await _productSevice.GetByCategoryId(new PagingRequest(categoryId,1, 102))??null;
            Guid brandId = (homeDTO.listBrands.FirstOrDefault()).Id.Value;
            homeDTO.ProductsByBrabds.brand = homeDTO.listBrands.FirstOrDefault()??null;
            homeDTO.ProductsByBrabds.products = await _productSevice.GetByBrandId(new PagingRequest(brandId, 1, 102))??null;


            return View(homeDTO);
        }

        /*public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
