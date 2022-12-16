using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaoDinhVu.DAL.Data;
using Entities.Models;
using CaoDinhVu.BLL.Services;
using Entities.Requests;
using Newtonsoft.Json;
using CaoDinhVu.BLL.Services.Implementations;
using Entities.DTOs;
using Entities.Responses;
using CaoDinhVu.WEB.Library;
using Microsoft.AspNetCore.Http;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : BaseAdminController
    {
        private readonly IOptionService _optionService;
        private readonly IColorService _colorService;
        private readonly IProductSevice _productSevice;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IUploadImage _uploadImage;
        private readonly IProductColorService _productColorService;
        private readonly IProductOptionService _productOptionService;

        public ProductsController(IOptionService optionService,IColorService colorService,IProductColorService productColorService,IProductOptionService productOptionService , IProductSevice productSevice, ICategoryService categoryService, IBrandService brandService, IUploadImage uploadImage, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _optionService = optionService;
            _colorService = colorService;
            _productSevice = productSevice;
            _categoryService = categoryService;
            _brandService = brandService;
            _uploadImage = uploadImage;
            _productColorService = productColorService;
            _productOptionService = productOptionService;

        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var product = await _productSevice.GetAll(2);
            return View(product);
        }
        public async Task<IActionResult> Trash()
        {
            var product = await _productSevice.GetAll(0);
            return View(product);
        }
        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productSevice.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BrandId"] = new SelectList( await _brandService.getAll(1), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(await _categoryService.getAll(1), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddImageItem(Guid id)
        {
            try
            {
                var img = Request.Form.Files["Image"];
                //Add Thumbnails
                var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["ImageItem"]);
                if(image == null)
                {
                    return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                }
                var result = await _productSevice.AddImageItem(id, image);
                if (!result.IsSuccess)
                {
                    return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                }
                return Json(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddImageThumbnails(Guid id, IFormCollection file)
        {
            try
            {
                //IFormFile file = form.Files[0];
                //var image = await _uploadImage.UploadImageToImgur(file);
                var image = String.Empty;

                if (image.Equals(String.Empty))
                {
                    return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                }
                var result = await _productSevice.ChangeThumbnails(id, image);
                if (!result.IsSuccess)
                {
                    return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                }
                return Json(JsonConvert.SerializeObject(new BaseResponse(true, image)));
            }
            catch (Exception ex)
            {
                return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImageItem(Guid id)
        {
            try
            {
                //Add Thumbnails
                var result = await _productSevice.DeleteImageItem(id);
                if (!result.IsSuccess)
                {
                    return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                }
                return Json(JsonConvert.SerializeObject(new BaseResponse(true, "Thành công")));
            }
            catch (Exception ex)
            {
                return Json(JsonConvert.SerializeObject(new BaseResponse(false, "Thất bại")));
                throw new Exception(ex.Message);
            }
        }
        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Thumbnails,Title,CategoryId,BrandId,Description,Price,Status,Screen,Camera,GeneralInformation,Charger,Battery,Connection,RAM,ROM,CPU,OperatingSystem")] ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                //Add Thumbnails
                var img = Request.Form.Files["Image"];
                if (img != null)
                {
                    var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);
                    product.Thumbnails = image;
                }
                //Add list image
                int i = 1;
                var listImg = new List<string>();
                while(Request.Form.Files[$"ImageSP_{i}"] != null)
                {
                    var image = await _uploadImage.UploadImageToImgur(Request.Form.Files[$"ImageSP_{i}"]);
                    listImg.Add(image.ToString());
                    i++;
                }
                product.Slug = XString.Str_Slug(product.Name);
                product.ListImage = listImg;
                product.Id = Guid.NewGuid();
                var result = await _productSevice.AddAsync(product);
                var productId = new Guid(result.Message);
                return Redirect("/Admin/products/AddProductItem" + "?productId=" + productId);
            }
            //ViewData["BrandId"] = new SelectList(await _brandService.getAll(), "Id", "Name", product.BrandId);
            //ViewData["CategoryId"] = new SelectList(await _categoryService.getAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var product = await _productSevice.GetById(id.Value);

            product.Screen = product.Detail.Screen;
            product.Camera = product.Detail.Camera;
            product.OperatingSystem = product.Detail.OperatingSystem;
            product.CPU = product.Detail.CPU;
            product.ROM = product.Detail.ROM;
            product.RAM = product.Detail.RAM;
            product.Connection = product.Detail.Connection;
            product.Battery = product.Detail.Battery;
            product.Charger = product.Detail.Charger;
            product.GeneralInformation = product.Detail.GeneralInformation;


            if (product == null)
            {
                return NotFound();
            }
            ViewBag.listBrand = new SelectList(await _brandService.getAll(1), "Id", "Name", product.BrandId);
            ViewBag.listCat = new SelectList(await _categoryService.getAll(1), "Id", "Name", product.CategoryId);
            ViewBag.Option = await _optionService.GetALL();
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(List<IFormFile> listImage, Guid id, [Bind("Name,Thumbnails,Title,CategoryId,BrandId,Description,Status,Id,Screen,Camera,GeneralInformation,Charger,Battery,Connection,RAM,ROM,CPU,OperatingSystem")] ProductRequest product)
        {
            if (ModelState.IsValid)
            {
                //Add Thumbnails
                var img = Request.Form.Files["Image"];
                if (img != null)
                {
                    var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);
                    product.Thumbnails = image;
                }
                //Add list image
                var listImg = new List<string>();
                if (listImage != null)
                {
                    foreach (var item in listImage)
                    {
                        var image = await _uploadImage.UploadImageToImgur(item);
                        listImg.Add(image.ToString());
                    }
                }
                product.ListImage=listImg;
                product.Slug = XString.Str_Slug(product.Name);
                var result = await _productSevice.Update(product);
               
                if (!result.IsSuccess)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(await _brandService.getAll(1), "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(await _categoryService.getAll(1), "Id", "Name", product.CategoryId);
            return View(product);
        }
        public async Task<IActionResult> AddProductItem(Guid productId)
        {
            if (productId == Guid.Empty)
                productId = new Guid("164c13be-77a8-4411-2e2e-08dac91bc8a4");
            var addProductItem = new AddProductItem();
            addProductItem.Product = await _productSevice.GetById(productId); 
            addProductItem.Colors = await _colorService.GetALL();
            addProductItem.Options = await _optionService.GetALL();
            return View(addProductItem);
        }
        public async Task<IActionResult> DeleteSoft(Guid categoryId)
        {
            var category = await _productSevice.DeleteSoft(categoryId);

            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(category));

        }
        public async Task<IActionResult> ChangeStatus(Guid categoryId)
        {
            var Result = await _productSevice.ChangeStatus(categoryId);


            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(Result));

        }
        /// <summary>
        /// Add product color
        /// </summary>
        /// <param name="productColor"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProductCorlor([FromBody] ProductColorRequest productColor)
        {
            var Result = await _productColorService.AddAsync(productColor);
            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(Result));

        }
        [HttpPost]
        public async Task<IActionResult> DeleteProductColor(Guid id)
        {
            var Result = await _productColorService.Delete(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatusProductColor(Guid id)
        {
            var Result = await _productColorService.ChangeStatus(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        /// <summary>
        /// Add product option
        /// </summary>
        /// <param name="productOption"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProductOption([FromBody] ProductOptionRequest productOption)
        {
            var Result = await _productOptionService.AddAsync(productOption);
            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(Result));

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductOption([FromBody] ProductOptionRequest productOption)
        {
            var Result = await _productOptionService.UpdateAsync(productOption);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProductOption(Guid id)
        {
            var Result = await _productOptionService.Delete(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatusProductOption(Guid id)
        {
            var Result = await _productOptionService.ChangeStatus(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] ColorDTO color)
        {
            var Result = await _colorService.AddAsync(color);
            if(Result.IsSuccess == true)
            {
                var colors = await _colorService.GetALL();
                return Json(JsonConvert.SerializeObject(
                    new Responses<ColorDTO>(
                        true,
                        "Thanh cong",
                        colors
                        )));
            }
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> AddOption([FromBody] OptionDTO option)
        {
            var Result = await _optionService.AddAsync(option);
            if (Result.IsSuccess == true)
            {
                var options = await _optionService.GetALL();
                return Json(JsonConvert.SerializeObject(
                    new Responses<OptionDTO>(
                        true,
                        "Thanh cong",
                        options
                        )));
            }
            return Json(JsonConvert.SerializeObject(Result));
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var Result = await _productSevice.Delete(id);
            return Json(JsonConvert.SerializeObject(Result));
        }

        
    }
}
