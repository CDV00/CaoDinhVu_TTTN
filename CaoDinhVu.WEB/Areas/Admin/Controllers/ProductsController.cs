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

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductSevice _productSevice;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IUploadImage _uploadImage;
        private readonly IProductColorService _productColorService;
        private readonly IProductOptionService _productOptionService;

        public ProductsController(IProductColorService productColorService,IProductOptionService productOptionService , IProductSevice productSevice, ICategoryService categoryService, IBrandService brandService, IUploadImage uploadImage)
        {
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
                product.ListImage = listImg;
                product.Id = Guid.NewGuid();
                var result = await _productSevice.AddAsync(product);
                return RedirectToAction(nameof(Index));
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
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.listBrand = new SelectList(await _brandService.getAll(1), "Id", "Name", product.BrandId);
            ViewBag.listCat = new SelectList(await _categoryService.getAll(1), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Thumbnails,Slug,Title,CategoryId,BrandId,Description,Price,Status,Id,CreateAt,CreateBy,UpdateAt,UpdateBy,IsActive,IsDelete")] ProductRequest product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


             
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

        public async Task<IActionResult> AddProductCorlor(ProductColorRequest productColor)
        {
            var Result = await _productColorService.AddAsync(productColor);
            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(Result));

        }
        public async Task<IActionResult> AddProductOption(ProductOptionRequest productColor)
        {
            var Result = await _productOptionService.AddAsync(productColor);
            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(Result));

        }


        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: Admin/Products/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        /*private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }*/
    }
}
