using CaoDinhVu.WEB.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController()
        {
            var a = HttpContext.Session.Get<UserDTO>("UserInfo").Id;
            if (HttpContext.Session.Get<UserDTO>("UserInfo").Id.Equals(Guid.Empty))
                RedirectToAction("Login", "Auth");
        }
        
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
