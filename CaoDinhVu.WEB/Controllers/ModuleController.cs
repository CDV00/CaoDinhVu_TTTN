using CaoDinhVu.BLL.Services;
using CaoDinhVu.WEB.Extensions;
using Entities.Constants;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Controllers
{
    public class ModuleController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductSevice _productSevice;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;
        private readonly IOptionService _optionService;
        private readonly ISliderService _sliderService;
        private readonly IUploadImage _uploadImage;


        public ModuleController(ICategoryService categoryService,
                              IProductSevice productSevice,
                              IUploadImage uploadImage,
                              IBrandService brandService,
                              IColorService colorService,
                              IOptionService optionService,
                              ISliderService sliderService)
        {
            _categoryService = categoryService;
            _productSevice = productSevice;
            _uploadImage = uploadImage;
            _brandService = brandService;
            _colorService = colorService;
            _optionService = optionService;
            _sliderService = sliderService;
        }
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> Menu()
        {
            MenuDTO menu = new MenuDTO();
            menu.listCategory = await _categoryService.getAll();
            menu.listBrands = await _brandService.getAll();
            return PartialView("_Menu", menu);
        }
        public async Task<IActionResult> Filter()
        {
            FilterDTO filter = new FilterDTO();
            filter.listCategory = await _categoryService.getAll();
            filter.listBrands = await _brandService.getAll();
            filter.listColors = await _colorService.GetALL();
            filter.listOptions = await _optionService.GetALL();
            return PartialView("Filter", filter);
        }
        public IActionResult _ShoppingCart()
        {
            ViewBag.CartCount = Carts.Count;
            return PartialView("_ShoppingCart");
        }

        public IActionResult _Account()
        {
            var userInfo = HttpContext.Session.Get<UserDTO>("UserInfo");

            return PartialView("_Account", userInfo);
        }

    }
}
