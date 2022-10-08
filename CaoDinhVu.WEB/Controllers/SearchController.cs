using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Controllers
{
    public class SearchController:Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}
