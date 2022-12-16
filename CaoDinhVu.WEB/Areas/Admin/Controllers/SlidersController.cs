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
using Entities.Constants;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : BaseAdminController
    {
        private readonly DBContext _context;
        private readonly ISliderService _sliderService;
        private readonly IUploadImage _uploadImage;

        public SlidersController(DBContext context, ISliderService sliderService, IUploadImage uploadImage, IHttpContextAccessor httpContextAccessor) :base(httpContextAccessor)
        {
            _context = context;
            _sliderService = sliderService;
            _uploadImage = uploadImage;
        }

        // GET: Admin/Sliders
        public async Task<IActionResult> Index()
        {
            return View((await _sliderService.getAll()).Where(s=>s.Status != 0));
        }
        public async Task<IActionResult> Trash()
        {
            return View((await _sliderService.getAll()).Where(s => s.Status == 0));
        }

        // GET: Admin/Sliders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _sliderService.GetById(id.Value);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Admin/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status")] SliderRequest slider)
        {
            if (ModelState.IsValid)
            {
                var img = Request.Form.Files["Image"];
                if (img != null)
                {
                    var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);
                    slider.Img = image;
                }

                slider.Id = Guid.NewGuid();
                await _sliderService.Create(slider);

                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Orders,Status,Id")] Slider slider)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }
            var img = Request.Form.Files["Image"];
            if (img != null)
            {
                var image = await _uploadImage.UploadImageToImgur(Request.Form.Files["Image"]);
                slider.Img = image;
            }

            if (ModelState.IsValid)
            {

                var result = await _sliderService.Update(slider);

                if (!result.IsSuccess)
                {
                    return View(slider);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSoft(Guid id)
        {
            var Result = await _sliderService.DeleteSoft(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Result = await _sliderService.Delete(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeOrder(Guid id, int? actionChange = 1)
        {
            var evenChange = ActionChangeOrder.UP;
            if (actionChange == 2)
            {
                evenChange = ActionChangeOrder.DOWN;
            }
            var result = await _sliderService.ChangeOrder(id, evenChange);

            return Json(JsonConvert.SerializeObject(result));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var Result = await _sliderService.ChangeStatus(id);
            return Json(JsonConvert.SerializeObject(Result));

        }

    }
}
