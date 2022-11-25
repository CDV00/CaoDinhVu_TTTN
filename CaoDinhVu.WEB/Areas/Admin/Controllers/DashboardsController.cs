using CaoDinhVu.BLL.Services;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardsController : Controller
    {
        private readonly IOrderService _orderService;

        public DashboardsController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            DashboardDTO dashboardDTO = new DashboardDTO();
            dashboardDTO.OrderInWeek = _orderService.GetOrderInWeek().Data;
            dashboardDTO.OrderByCategory = _orderService.GetOrderByCategory().Data;
            dashboardDTO.OrderByBrand = _orderService.GetOrderByBrand().Data;
            return View(dashboardDTO);
        }
    }
}
