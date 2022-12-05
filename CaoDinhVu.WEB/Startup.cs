using Entities.Extensions;
using CaoDinhVu.WEB.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using CaoDinhVu.WEB.Controllers;

namespace CaoDinhVu.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            /*services.AddDbContext<DBContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });*/



            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "Cart";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
            });

            /*services.AddControllers()
                  .AddNewtonsoftJson(options =>
                      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                   );*/
            //services.AddOutputCaching();
            services.AddHttpContextAccessor();

            //[OutputCache]
            services.AddControllersWithViews(x =>
            {
                x.CacheProfiles.Add("sitefinity", new CacheProfile()
                {
                    Duration = 60 * 60, // cache the response for 1 hour
                    VaryByHeader = "Accept-Encoding",
                    Location = ResponseCacheLocation.Any,
                });
            });

            //services.AddOutputCaching();


            services.AddRazorPages();
            services.AddControllersWithViews();
            services.ConfigureCos();
            services.ConfigureIISIntegration();
            services.ConfigureIdentity();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositories();
            services.ConfigureServices();
            services.AddAutoMapper(typeof(MapperInitializer));

            services.AddMvc()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            /*app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Chi-tiet-san-pham",
                    template: "chi-tiet-san-pham/{id}",
                    defaults: (Controllers: "Product", Action: "ProductDetail", id: new Guid()));
                
            });*/

            app.UseEndpoints(endpoints =>
            {
                //Dashboard
                endpoints.MapAreaControllerRoute(
                     name: "quan-ly-thong-ke",
                     areaName: "Admin",
                     defaults: new { Areas = "Admin", Controller = "Dashboards", Action = "Index" },
                     pattern: "quan-ly-thong-ke");
                //đặt hành
                //sản phẩm
                //thương hiệu
                endpoints.MapAreaControllerRoute(
                   name: "MyAreaProductsd",
                   areaName: "Admin",
                   defaults: new { Areas = "Admin", Controller = "Brand", Action = "Index" },
                   pattern: "quan-ly-thuong-hieu");
                //danh mục
                endpoints.MapAreaControllerRoute(
                     name: "xem-tat-ca",
                     areaName: "Admin",
                     defaults: new { Areas = "Admin", Controller = "Categories", Action = "Index" },
                     pattern: "quan-ly-danh-muc");
                endpoints.MapAreaControllerRoute(
                     name: "xem-chi-tiet-danh-muc",
                     areaName: "Admin",
                     defaults: new { Areas = "Admin", Controller = "Categories", Action = "Details" },
                     pattern: "danh-muc/chi-tiet/{id?}");
                endpoints.MapAreaControllerRoute(
                   name: "MyAreaProducts",
                   areaName: "Admin",
                   defaults: new { Areas = "Admin", Controller = "Categories", Action = "Edit" },
                   pattern: "sua-danh-muc/{id?}");
                /*endpoints.MapAreaControllerRoute(
                    name: "MyAreaProducts",
                    areaName: "Admin",
                    defaults: new {Areas = "Admin", Controller = "Categories", Action = "Edit" },
                    pattern: "sua-danh-muc/{id?}");*/

            });
            /*app.Use(async (context, next) =>
            {
                await next.Invoke();
                if (context.Response.StatusCode == 401)
                {
                    context.Response.Redirect("~/Auth/Login");
                }
            });*/
            /*app.UseWhen(ShouldAuthenticate, appBuilder =>
            {
                appBuilder.UseMiddleware<AccountController>();
            });*/

            //app
            app.RegisterRoute();

            app.Run(handler: async (conttext) =>
            {
                if (conttext.Response.StatusCode == 403)
                {
                    conttext.Response.Redirect("~/Auth/Login");
                }
                await conttext.Response.WriteAsync(text: "Failse to find route");
            });
        }
    }
}
