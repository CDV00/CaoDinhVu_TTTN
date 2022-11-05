using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaoDinhVu.BLL.Services;

namespace CaoDinhVu.WEB.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandService.getAll();
            return View(brands);
        }
        

    }
}
