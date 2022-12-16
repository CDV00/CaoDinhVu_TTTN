using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaoDinhVu.DAL.Data;
using Entities.Requests;
using CaoDinhVu.BLL.Services;
using System.IO;
using Microsoft.AspNetCore.Http;
using CaoDinhVu.WEB.Library;
using Newtonsoft.Json;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IUploadImage _uploadImage;

        public CategoriesController(ICategoryService categoryService, IUploadImage uploadImage, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _categoryService = categoryService;
            _uploadImage = uploadImage;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.getAll(2));
        }
        public async Task<IActionResult> Trash()
        {
            return View(await _categoryService.getAll(0));
        }
        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.getById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Orders,Image,Title,Status")] CategoryRequest category)
        {
            if (ModelState.IsValid)
            {
                var image =await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);
                
                category.Image = image;
                category.Id = Guid.NewGuid();
                category.ParentId = Guid.NewGuid();
                //category.Status = 1;
                category.Slug = XString.Str_Slug(category.Name);
                category.CreateBy = new Guid();
                await _categoryService.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        //public IUploadImage
        public string UploadImage(IFormFile files)
        {
            //long size = files.Sum(f => f.Length);

            // full path to file in temp location
            string filePaths = "";
            if (files.Length > 0)
            {
                filePaths = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\" + files.FileName);
                using (var stream = new FileStream(filePaths, FileMode.Create))
                {
                    files.CopyTo(stream);
                }
            }


            return filePaths;
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.getById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Orders,Image,Title,Status,Id")] CategoryRequest category)
        {
            var img = Request.Form.Files["Image"];
            if (img != null)
            {
                var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);
                category.Image = image;
            }
            if (!_categoryService.CheckExist(category.Id.Value))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                /*if (category.CreateBy == null)
                    category.CreateBy = new Guid();*/
                var Result = await _categoryService.Update(category);
                if (!Result.IsSuccess)
                {
                    return RedirectToAction(nameof(Edit));
                }
               
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var category = await _categoryService.Delete(id.Value);
            return Json(JsonConvert.SerializeObject(category));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSoft(Guid categoryId)
        {
            var category = await _categoryService.DeleteSoft(categoryId);
            return Json(JsonConvert.SerializeObject(category));

        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(Guid categoryId)
        {
            var Result = await _categoryService.ChangeStatus(categoryId);
            return Json(JsonConvert.SerializeObject(Result));

        }

        
    }
}
