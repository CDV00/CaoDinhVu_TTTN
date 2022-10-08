using CaoDinhVu.BLL.Services;
using CaoDinhVu.BLL.Services.Implementations;
using CaoDinhVu.DAL.Data;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Extensions
{
    public static class ServiceExtension
    {
        public static object Configuration { get; private set; }

        public static void ConfigureCos(this IServiceCollection services)
        {
            // Allows requests from any source
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination");
                });
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                //Configure Password
                options.Password.RequireDigit = true; // Not mandatory digit
                options.Password.RequireLowercase = false; // Not mandatory lowercase
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3; // minimum password length
                options.Password.RequiredUniqueChars = 1; //

                // Configure Lockout - Lock user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // lock in 5minutes
                options.Lockout.MaxFailedAccessAttempts = 5; // failed 5 times => lock
                options.Lockout.AllowedForNewUsers = true;

                // Configure User.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email is unique

                // Configure Login.
                //Configure verify email (email existed is required)
                options.SignIn.RequireConfirmedEmail = false;
                // Verify phone number
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddEntityFrameworkStores<DBContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>
            {
                //CVPANHTNT6-59
                options.UseSqlServer(configuration.GetConnectionString("MyDB"), b =>
                b.MigrationsAssembly("CaoDinhVu.WEB"));
            });
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
