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

        public ProductController(IProductSevice productSevice, IProductColorService productColorService, IProductOptionService productOptionService)
        {
            _productSevice = productSevice;
            _productColorService = productColorService;
            _productOptionService = productOptionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var listProduct = await _productSevice.GetAll(new PagingRequest(1,2));
            return View(listProduct);
        }
        public async Task<IActionResult> GetProductByBrandId(Guid id)
        {
            var listProduct = await _productSevice.GetByBrandId(new PagingRequest(id));
            ViewBag.Title = listProduct.Data.FirstOrDefault().Brand.Name;
            return View(listProduct);
        }
        public async Task<IActionResult> GetProductByCategoryId(Guid id)
        {
            var listProduct = await _productSevice.GetByCategoryId(new PagingRequest(id,1, 2));
            ViewBag.Title = listProduct.Data.FirstOrDefault().Category.Name;
            return View(listProduct);
        }
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            Guid s = Guid.Parse("128A4BCD-E90A-499B-8A92-45D07D1CC4FE");
            id = id.Equals(Guid.Empty) ? Guid.Parse("128A4BCD-E90A-499B-8A92-45D07D1CC4FE"):id;
            var product = await _productSevice.GetById(id);
            Product.ProductColorId = product.ProductColors.FirstOrDefault().Id;
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
                Price = price,
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
