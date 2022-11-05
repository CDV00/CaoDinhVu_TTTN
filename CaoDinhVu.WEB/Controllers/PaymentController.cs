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

namespace CaoDinhVu.WEB.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrderService _orderService;

        public PaymentController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var Carts = HttpContext.Session.Get<List<CartItem>>("Cart");
            return View(Carts);
        }

        public async Task<IActionResult> DoPayment(IFormCollection field)
        {
            string name = field["name"];
            string phone = field["phone"];
            string email = field["email"];
            string tinh = field["Tinh"];
            string huyen = field["Huyen"];
            string phuong = field["phuong"];
            string soNha = field["soNha"];
            string ghiChu = field["ghiChu"];
            var carts = HttpContext.Session.Get<List<CartItem>>("Cart");

            var paymentRequest = new PaymentRequest()
            {
                Name = name,
                Phone = phone,
                Email = email,
                Address = soNha + ", " + phuong + ", " + huyen + ", " + tinh,
                Note = ghiChu,
                Carts = carts
            };
            var Result = await _orderService.Add(paymentRequest);

            if (!Result.IsSuccess)
            {
                return View("Index");
            }
            carts.Clear();
            HttpContext.Session.Set<List<CartItem>>("Cart", carts);
            return RedirectToAction("/gio-hang");
        }
    }
}
