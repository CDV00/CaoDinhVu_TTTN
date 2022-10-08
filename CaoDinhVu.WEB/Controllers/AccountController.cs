using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // GET: /Account/Profile-Address
        public IActionResult ProfileAddress()
        {
            return View();
        }
        // GET: /Account/Profile-Main
        public IActionResult ProfileMain()
        {
            return View();
        }
        // GET: /Account/Profile-Order
        public IActionResult ProfileOrder()
        {
            return View();
        }
        // GET: /Account/Profile-Seller
        public IActionResult ProfileSeller()
        {
            return View();
        }
        // GET: /Account/Profile-Setting
        public IActionResult ProfileSetting()
        {
            return View();
        }
    }
}
