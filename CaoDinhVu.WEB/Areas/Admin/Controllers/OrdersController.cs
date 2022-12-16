using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaoDinhVu.DAL.Data;
using Entities.Models;
using CaoDinhVu.BLL.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : BaseAdminController
    {
        //private readonly DBContext _context;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _orderService = orderService;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var result = await _orderService.GetAll(2);
            return View(result.Data);
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetDetailById(id.Value);
            if (!order.IsSuccess)
            {
                return NotFound();
            }

            return View(order.Data);
        }

        /*// GET: Admin/Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TotalPrice,FirstName,LastName,Country,Address,PhoneNumber,StatusOrder,Id,CreateAt,CreateBy,UpdateAt,UpdateBy,IsActive,IsDelete")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }*/

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetDetailById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        public async Task<IActionResult> DeleteSoft(Guid orderId)
        {
            var order = await _orderService.DeleteSoft(orderId);

            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(order));

        }
        public async Task<IActionResult> ChangeStatus(Guid orderId)
        {
            var Result = await _orderService.ChangeStatus(orderId);


            /*object Result = new
            {
                Status = true

            };*/
            return Json(JsonConvert.SerializeObject(Result));

        }
        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TotalPrice,FirstName,LastName,Country,Address,PhoneNumber,StatusOrder,Id,CreateAt,CreateBy,UpdateAt,UpdateBy,IsActive,IsDelete")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_orderService.Update(order);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }*/

        // GET: Admin/Orders/Delete/5
        /*public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }*/

        // POST: Admin/Orders/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }*/
    }
}
