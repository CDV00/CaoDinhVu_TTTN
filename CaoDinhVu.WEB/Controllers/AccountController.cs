using CaoDinhVu.WEB.Extensions;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CaoDinhVu.BLL.Services;

namespace CaoDinhVu.WEB.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IOrderService _orderService;

        //private readonly IHttpContextAccessor _HttpContext;

        public AccountController(IHttpContextAccessor httpContextAccessor, IAccountService accountService, IOrderService orderService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _orderService = orderService;
            /*try
            {
                var a = _httpContextAccessor.HttpContext.Session.Get<UserDTO>("UserInfo").Id??Guid.Empty;
                if (a.Equals(Guid.Empty))
                    _httpContextAccessor.HttpContext.Response.Redirect("dLogin");
            }
            catch (NullReferenceException)
            {
                _httpContextAccessor.HttpContext.Response.Redirect("dLogin");
            }*/
        }
        //trả về trang login ở controller 
        /*public void checkUser()
        {
            try
            {
                var userId = UserInfo.Id ?? Guid.Empty;
                if (UserInfo.Id.Equals(Guid.Empty))
                    RedirectToAction("Login", "Auth");
            }
            catch (NullReferenceException ex)
            {
                RedirectToAction("Login", "Auth");
            }
            
        }*/
        public IActionResult Index()
        {
            Guid UserId = UserInfo.Id??new Guid("62e7da25-2472-4410-5f95-08dab98aa38a");

            return View();
        }
        // GET: /Account/Profile-Address
        public IActionResult ProfileAddress()
        {
            /*bool checkUserId = UserInfo.Id.Equals(Guid.Empty);
            if (checkUserId)
                HttpContext.Response.Redirect("CheckUser");*/
            /*try
            {
                bool checkUserId = UserInfo.Id.Equals(Guid.Empty);
                if (checkUserId == true)
                    return RedirectToAction("Login", "Auth");
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Login", "Auth");
            }*/
            //var a = HttpContext.Session.Get<UserDTO>("UserInfo").Id;
            /*if (UserInfo.Id.Equals(Guid.Empty))
                RedirectToAction("Login", "Auth");*/
            //return RedirectToAction("Login", "Auth");
            return View();
        }
        // GET: /Account/Profile-Main
        public async Task<IActionResult> ProfileMain()
        {
            Guid UserId = (UserInfo.Id.Equals(Guid.Empty))? new Guid("62e7da25-2472-4410-5f95-08dab98aa38a"): UserInfo.Id.Value;
            var profileMain = new ProfileMain();

            profileMain.AmountOrder = await _orderService.CountOrderByStatus(UserId);
            profileMain.AmountWaitForConfirmation = await _orderService.CountOrderByStatus(UserId,1);
            profileMain.AmountAwaitingDelivery = await _orderService.CountOrderByStatus(UserId,2);
            profileMain.AmountDeliveredItems = await _orderService.CountOrderByStatus(UserId,3);
            profileMain.AppUser =_accountService.GetById(UserId).Result.Data;
            profileMain.Order = _orderService.GetByUserId(UserId).Result.Data;
            return View(profileMain);
        }
        // GET: /Account/Profile-Order
        public async Task<IActionResult> ProfileOrder()
        {
            Guid UserId = (UserInfo.Id.Equals(Guid.Empty)) ? new Guid("62e7da25-2472-4410-5f95-08dab98aa38a") : UserInfo.Id.Value;

            var orders = await _orderService.GetByUserId(UserId);
            return View(orders.Data);
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
        public IActionResult LogOut()
        {
            HttpContext.Session.Set<UserDTO>("UserInfo",null);
            return Redirect("~/");
        }
    }
}
