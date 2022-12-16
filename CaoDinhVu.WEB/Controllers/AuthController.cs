using CaoDinhVu.BLL.Services;
using CaoDinhVu.WEB.Extensions;
using Entities.DTOs;
using Entities.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace CaoDinhVu.WEB.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoRegister(IFormCollection field)
        {
            string fullName = field["fullName"];
            string userName = field["userName"];
            string email = field["email"];
            string tinh = field["Tinh"];
            string huyen = field["Huyen"];
            string phuong = field["phuong"];
            string soNha = field["soNha"];
            string gender = field["genders"];
            string password = field["password"];
            string repeatPssword = field["repeatPssword"];

            if(password != repeatPssword)
            {
                ViewBag.Error = "<strong class=\"text-danger \">Nhập lại mật khẩu chưa trùng khớp.</strong>";
                return View("Register");

            }

            var registerRequest = new RegisterRequest() {
                Email = email,
                FullName = fullName,
                UserName = userName,
                Address = soNha + ", " + phuong + ", " + huyen + ", " + tinh,
                Gender = gender,
                Password = password,
                PasswordConfirm = repeatPssword
            };
            var result = await _accountService.Register(registerRequest);
            if (!result.IsSuccess)
            {
                ViewBag.Error = "<strong class=\"text-danger \">"+result.Message+".</strong>";
                return View("Register");
            }

            ViewBag.Error = "<strong class=\"text-success \">" + result.Message + ".</strong>";
            return View("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DoLogin(IFormCollection field)
        {
            string email = field["userName"];
            string password = field["password"];

            var loginRequest = new LoginRequest()
            {
                Email = email,
                Password = password
            };

            var result = await _accountService.Login(loginRequest);
            if (!result.IsSuccess)
            {
                ViewBag.Error = "<strong class=\"text-danger \">" + result.Message + ".</strong>";
                return View("Login");
            }

            var userInfo = UserInfo;
            userInfo = result.Data;
            userInfo.Id = result.Data.Id;
            HttpContext.Session.Set<UserDTO>("UserInfo", userInfo);

            ViewBag.Error = "<strong class=\"text-success \">" + result.Message + ".</strong>";
            if(result.Data.Role != 3)
            {
                return Redirect("/quan-ly-thong-ke");
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
