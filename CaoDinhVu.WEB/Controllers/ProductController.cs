using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaoDinhVu.BLL.Services;
using Entities.Constants;
//using System.Web.Helpers;
using Newtonsoft.Json;
using Entities.DTOs;
using Entities.Requests;

namespace CaoDinhVu.WEB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductSevice _productSevice;
        private readonly IProductColorService _productColorService;
        private readonly IProductOptionService _productOptionService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(IProductSevice productSevice, IProductColorService productColorService, IProductOptionService productOptionService, IBrandService brandService,ICategoryService categoryService)
        {
            _productSevice = productSevice;
            _productColorService = productColorService;
            _productOptionService = productOptionService;
            _categoryService = categoryService;
            _brandService = brandService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll(int? page = 1)
        {
            var listProduct = await _productSevice.GetAll(new PagingRequest(page.Value,12));
            return View(listProduct);
        }
        public async Task<IActionResult> GetProductByBrandId(Guid id, int? page = 1)
        {
            ViewBag.id = id;
            var listProduct = await _productSevice.GetByBrandId(new PagingRequest(id, page.Value, 12));
            ViewBag.Title =(await _brandService.getById(id)).Name;
            return View(listProduct);
        }
        public async Task<IActionResult> GetProductByCategoryId(Guid id, int? page = 1)
        {
            ViewBag.id = id;
            var listProduct = await _productSevice.GetByCategoryId(new PagingRequest(id,page.Value, 12));
            ViewBag.Title = (await _categoryService.getById(id)).Name;
            return View(listProduct);
        }
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            Guid s = Guid.Parse("128A4BCD-E90A-499B-8A92-45D07D1CC4FE");
            id = id.Equals(Guid.Empty) ? Guid.Parse("128A4BCD-E90A-499B-8A92-45D07D1CC4FE"):id;
            var product = await _productSevice.GetById(id, 1);
            Product.ProductColorId = product.ProductColors.Where(se=>se.Status == 1).FirstOrDefault().Id.Value;
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeColor(string productId, string colorId)
        {
            Guid ProductId = Guid.Parse(productId);
            Guid ColorId = Guid.Parse(colorId);
            Product.ProductColorId = _productColorService.GetProductColorId(ProductId, ColorId);
            List<OptionDTO> options = new List<OptionDTO>();
            options = await _productOptionService.getOptionbyProductcolorId(Product.ProductColorId);
           
            return Json(JsonConvert.SerializeObject(options));
        }
        [HttpPost]
        public async Task<JsonResult> UpdatePrice(string productId, string optionId, string colorId)
        {
            Guid ProductId = Guid.Parse(productId);
            Guid OptionId = Guid.Parse(optionId);
            Guid ColorId = Guid.Parse(colorId);
            Guid productColorId = _productColorService.GetProductColorId(ProductId, ColorId);
            decimal price = await _productOptionService.GetPrice(productColorId, OptionId);
            /*List<string> options = new List<string>();
            options.Add("a");*/
            return Json(new
            {
                Price = price.ToString("#,##0"),
                status = true,

            });
            //return Json(JsonConvert.SerializeObject(options));
        }

        // public async Task<IActionResult> SimilarProducts()
        /* public async Task<IActionResult> FiProducts()
         {
             return View();
         }*/


    }
}
