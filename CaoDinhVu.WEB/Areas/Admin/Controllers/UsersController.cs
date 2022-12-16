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
using Microsoft.AspNetCore.Http;
using Entities.Requests;
using Newtonsoft.Json;

namespace CaoDinhVu.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : BaseAdminController
    {
        private readonly IAccountService _accountService;
        private readonly DBContext _context;

        public UsersController(DBContext context, IAccountService accountService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _accountService = accountService;
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await _accountService.GetAll();
            Guid IdAdmin = new Guid("228a4bcd-e90a-499b-8a92-45d07d1cc4fe");
            return View(users.Where(m=>m.Id != IdAdmin && m.Status != 0));
        }
        public async Task<IActionResult> Trash()
        {
            var users = await _accountService.GetAll();
            return View(users.Where(m => m.Status == 0));
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = (await _accountService.GetById(id.Value)).Data;
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvatarUrl,Fullname,FirstName,LastName,ProfileLink,FacebookLink,LinkedlnLink,YoutubeLink,HeadLine,Description,Address,Gender,RefreshToken,RefreshTokenExpiryTime,CreateAt,CreateBy,UpdateAt,UpdateBy,IsActive,IsDelete,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                appUser.Id = Guid.NewGuid();
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _accountService.GetProfileSetting(id.Value);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser.Data);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id, IFormCollection field, [Bind("Id,Status,Role")] UserRequest userRequest)
        {
            string fullName = field["fullName"];
            string userName = field["userName"];
            string email = field["email"];
            string tinh = field["Tinh"];
            string huyen = field["Huyen"];
            string phuong = field["phuong"];
            string soNha = field["soNha"];
            string gender = field["genders"];




            userRequest.Email = email;
            userRequest.FullName = fullName;
            userRequest.UserName = userName;
            userRequest.Address = soNha + ", " + phuong + ", " + huyen + ", " + tinh;
            userRequest.Gender = gender;

            var result = await _accountService.UpdateProfile(userRequest);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            var appUser = await _accountService.GetProfileSetting(id);
            return View(appUser);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Result = await _accountService.Delete(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSoft(Guid id)
        {
            var Result = await _accountService.DeleteSoft(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var Result = await _accountService.ChangeStatus(id);
            return Json(JsonConvert.SerializeObject(Result));
        }
        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appUser = await _context.AppUsers.FindAsync(id);
            _context.AppUsers.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(Guid id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }
    }
}
