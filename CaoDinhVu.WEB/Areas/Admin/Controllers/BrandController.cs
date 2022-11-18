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
using CaoDinhVu.WEB.Library;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IUploadImage _uploadImage;

        public BrandController(IBrandService brandService, IUploadImage uploadImage)
        {
            _brandService = brandService;
            _uploadImage = uploadImage;
        }

        // GET: Admin/Brand
        public async Task<IActionResult> Index()
        {
            return View(await _brandService.getAll());
        }

        // GET: Admin/Brand/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandService.getById(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Slug,ParentId,Orders,Image,Title,Status,Id,CreateAt,CreateBy,UpdateAt,UpdateBy,IsActive,IsDelete")] BrandRequest brand)
        {
            if (ModelState.IsValid)
            {
                var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);

                brand.Image = image;
                brand.Id = Guid.NewGuid();
                brand.ParentId = Guid.NewGuid();
                //category.Status = 1;
                brand.Slug = XString.Str_Slug(brand.Name);
                brand.CreateBy = new Guid("228a4bcd-e90a-499b-8a92-45d07d1cc4fe");

                var Result = await _brandService.AddAsync(brand);
                if(Result.IsSuccess)
                    return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Admin/Brand/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandService.getById(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Slug,ParentId,Orders,Image,Title,Status,Id,CreateAt,CreateBy,UpdateAt,UpdateBy,IsActive,IsDelete")] BrandRequest brand)
        {
            var img = Request.Form.Files["Image"];
            if (img != null)
            {
                var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);
                brand.Image = image;
            }
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _brandService.Update(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_brandService.CheckExist(brand.Id.Value))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        public async Task<IActionResult> DeleteSoft(Guid categoryId)
        {
            var category = await _brandService.DeleteSoft(categoryId);

            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(category));

        }
        public async Task<IActionResult> ChangeStatus(Guid categoryId)
        {
            var Result = await _brandService.ChangeStatus(categoryId);


            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(Result));

        }

        // GET: Admin/Brand/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandService.getById(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var brand = await _brandService.getById(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
