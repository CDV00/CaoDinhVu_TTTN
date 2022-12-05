using CaoDinhVu.WEB.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Controllers
{
    public class BaseController : Controller
    {
        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Cart");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }
        public UserDTO UserInfo
        {
            get
            {
                var data = HttpContext.Session.Get<UserDTO>("UserInfo");
                if (data == null)
                {
                    data = new UserDTO();
                }
                return data;
            }
        }
        public IActionResult dLogin()
        {
            return RedirectToAction("Login", "Auth");
        }

    }
}
