﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Extensions
{
    public static class RouteConfig
    {
        public static void RegisterRoute(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                // Đăng ký
                endpoints.MapControllerRoute(
                    name: "dang-nhap",
                    defaults: new { controller = "Auth", action = "Login" },
                    pattern: "dang-nhap");
                // Đăng ký
                endpoints.MapControllerRoute(
                    name: "dang-ky",
                    defaults: new { controller = "Auth", action = "Register" },
                    pattern: "dang-ky");
                // Gio hang
                endpoints.MapControllerRoute(
                    name: "gio-hang",
                    defaults: new { controller = "Cart", action = "index" },
                    pattern: "gio-hang");
                // thanh toán
                endpoints.MapControllerRoute(
                    name: "thanh-toan",
                    defaults: new { controller = "Payment", action = "index" },
                    pattern: "thanh-toan");
                /*endpoints.MapControllerRoute(
                   name: "tim kiem paging",
                   defaults: new { controller = "Search", action = "index" },
                   pattern: "tim-kiem/{keyWork}/{page?}");

                endpoints.MapControllerRoute(
                    name: "tim kiem",
                    defaults: new { controller = "Search", action = "index" },
                    pattern: "tim-kiem/{keyWork}");*/
                endpoints.MapControllerRoute(
                    name: "tim kiem a",
                    defaults: new { controller = "Search", action = "index" },
                    pattern: "tim-kiem");
                //danh muc
                endpoints.MapControllerRoute(
                    name: "Tat ca danh muc",
                    defaults: new { controller = "Category", action = "Index" },
                    pattern: "danh-muc");


                //thuong hieu


                endpoints.MapControllerRoute(
                    name: "Tat ca thuong hieu",
                    defaults: new { controller = "Brand", action = "GetAll" },
                    pattern: "thuong-hieu");
                //san pham
                endpoints.MapControllerRoute(
                    name: "Danh muc",
                    defaults: new { controller = "Product", action = "GetProductByCategoryId" },
                    pattern: "danh-muc/{id:Guid}");

                endpoints.MapControllerRoute(
                    name: "Thuong hieu",
                    defaults: new { controller = "Product", action = "GetProductByBrandId" },
                    pattern: "thuong-hieu/{id:Guid}");

                endpoints.MapControllerRoute(
                    name: "Tat ca san pham",
                    defaults: new { controller = "Product", action = "GetAll" },
                    pattern: "tat-ca-san-pham");

                endpoints.MapControllerRoute(
                    name: "Chi tiet san pham",
                    defaults: new { controller = "Product", action = "ProductDetail" },
                    pattern: "chi-tiet-san-pham/{id:Guid}");
                //

                //route Admin
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action}");
                //routo default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}