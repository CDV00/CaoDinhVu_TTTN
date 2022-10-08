using CaoDinhVu.DAL.Data;
using CaoDinhVu.WEB.Library;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Data
{
    public static class Seed
    {
        public static async Task SeedDataAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, DBContext appContext)
        {
            var AdminRoleId = Guid.NewGuid();
            var SellerRoleId = Guid.NewGuid();
            var CustomerRoleId = Guid.NewGuid();
            var adminId = new Guid("228a4bcd-e90a-499b-8a92-45d07d1cc4fe");
            //seed Users
            if (!await userManager.Users.AnyAsync())
            {
                var userAdmin = new AppUser
                {
                    Id = adminId,
                    UserName = "admin123",
                    Email = "admin123@gmail.com",
                    NormalizedEmail = "admin123@gmail.com",
                };

                var roles = new List<IdentityRole<Guid>>
                {
                new IdentityRole<Guid>{Name = UserRoles.Admin,Id = AdminRoleId,NormalizedName=UserRoles.Admin},
                new IdentityRole<Guid>{Name = UserRoles.Seller, Id = SellerRoleId,NormalizedName=UserRoles.Seller},
                new IdentityRole<Guid>{Name = UserRoles.Customer, Id = CustomerRoleId, NormalizedName = UserRoles.Customer },
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                //foreach (var user in users)
                //{
                //user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(userAdmin, "123");
                await userManager.AddToRoleAsync(userAdmin, UserRoles.Admin);
                //}
                //add user Sellers
                var userSellers = new List<AppUser>
                {
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753501"),
                        Fullname = "Elon Musk",
                        UserName = "ElonMusk@gmail.com",
                        Email = "ElonMusk@gmail.com",
                        NormalizedEmail = "ElonMusk@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753502"),
                        Fullname = "Jeff Bezos",
                        UserName = "JeffBezos@gmail.com",
                        Email = "JeffBezos@gmail.com",
                        NormalizedEmail = "JeffBezos@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753503"),
                        Fullname = "Bernard Arnault",
                        UserName = "BernardArnault@gmail.com",
                        Email = "BernardArnault@gmail.com",
                        NormalizedEmail = "BernardArnault@gmail.com",
                    }

                };
                foreach (var userSeller in userSellers)
                {
                    await userManager.CreateAsync(userSeller, "123");
                    await userManager.AddToRoleAsync(userSeller, UserRoles.Seller);
                }
                //add user Customer
                var userCustomers = new List<AppUser>
                {
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753504"),
                        Fullname = "Bill Gates",
                        UserName = "BillGates@gmail.com",
                        Email = "BillGates@gmail.com",
                        NormalizedEmail = "BillGates@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753505"),
                        Fullname = "Larry Page",
                        UserName = "LarryPage@gmail.com",
                        Email = "LarryPage@gmail.com",
                        NormalizedEmail = "LarryPage@gmail.com",
                    },
                    new AppUser
                    {

                        Id = new Guid("9e47da02-3d3e-428d-a207-d53908753506"),
                        Fullname = "Sergey Brin",
                        UserName = "SergeyBrin@gmail.com",
                        Email = "SergeyBrin@gmail.com",
                        NormalizedEmail = "SergeyBrin@gmail.com",
                    }

                };
                foreach (var userCustomer in userCustomers)
                {
                    await userManager.CreateAsync(userCustomer, "123");
                    await userManager.AddToRoleAsync(userCustomer, UserRoles.Customer);
                }
                //
            }
            //seed Categories
            if (!await appContext.Categories.AnyAsync())
            {
                await appContext.Categories.AddAsync(new Category(new Guid("228a4bcd-e90a-499b-8a92-15d07d1cc4fe"), "Điện thoại", XString.Str_Slug("Điện thoại"), 1, adminId));
                await appContext.Categories.AddAsync(new Category(new Guid("228a4bcd-e90a-499b-8a92-25d07d1cc4fe"), "Máy tính xách tay", XString.Str_Slug("Máy tính xách tay"), 1, adminId));
                await appContext.Categories.AddAsync(new Category(new Guid("228a4bcd-e90a-499b-8a92-35d07d1cc4fe"), "Máy tính bảng", XString.Str_Slug("Máy tính bảng"), 1, adminId));
            }
            //seed Brands
            if (!await appContext.Brands.AnyAsync())
            {
                await appContext.Brands.AddAsync(new Brand(new Guid("228a4bcd-e90a-499b-8a92-15d07d1cc4fe"), "Apple", XString.Str_Slug("Apple"), 1, adminId));
                await appContext.Brands.AddAsync(new Brand(new Guid("228a4bcd-e90a-499b-8a92-25d07d1cc4fe"), "Samsung", XString.Str_Slug("Samsung"), 1,adminId));
                await appContext.Brands.AddAsync(new Brand(new Guid("228a4bcd-e90a-499b-8a92-35d07d1cc4fe"), "Lenovo", XString.Str_Slug("Lenovo"), 1,adminId));
            }
        }
    }
}
