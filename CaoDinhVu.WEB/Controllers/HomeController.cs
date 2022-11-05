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
            homeDTO.listProducts = await _productSevice.GetAll(new PagingRequest(1,2));
            Guid categoryId = (homeDTO.listCategory.FirstOrDefault()).Id;
            homeDTO.listProductsByCategory = await _productSevice.GetByCategoryId(new PagingRequest(categoryId,1, 2));
            Guid brandId = (homeDTO.listCategory.FirstOrDefault()).Id;
            //PagingRequest paging
            homeDTO.listProductsByBrabd = await _productSevice.GetByBrandId( new PagingRequest(brandId, 1, 2));


            //bool res = (await _brandService.SeedMail("caodinhvu00@gmail.com", "vucao005@gmail.com", "Gửi mail", "Nội dung Email")).IsSuccess; 
            /*string uri = @"D:\HK3_2022\ASP\CaoDinhVu\CaoDinhVu\Content\images\banners";
            List<string> img = new List<string>();
            img.Add(uri+ @"\slider_01.png");
            img.Add(uri+ @"\slider_02.png");
            img.Add(uri+ @"\slider_03.png");
            foreach (var item in img)
            {
                string url = await _uploadImage.UploadImageToImgur(item);
                Console.WriteLine(url);
            }*/


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
