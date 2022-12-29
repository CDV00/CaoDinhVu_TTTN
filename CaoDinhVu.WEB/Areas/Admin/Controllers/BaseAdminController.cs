using CaoDinhVu.WEB.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseAdminController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            try
            {
                var UserId = _httpContextAccessor.HttpContext.Session.Get<UserDTO>("UserInfo").Id ?? Guid.Empty;
                var Role = _httpContextAccessor.HttpContext.Session.Get<UserDTO>("UserInfo").Role;
                if (UserId.Equals(Guid.Empty) || Role == 3)
                {
                    ViewBag.Error = "<strong class=\"text-danger \">Bạn không có quyền admin</strong>";
                    _httpContextAccessor.HttpContext.Response.Redirect("/dang-nhap");
                }
                   
            }
            catch (NullReferenceException)
            {
                _httpContextAccessor.HttpContext.Response.Redirect("/dang-nhap");
            }
        }
        /*public IActionResult dlogin()
        {
            return Redirect("/dang-nhap");
            //return RedirectToAction("Login", "Auth");
        }*/
    }
}
