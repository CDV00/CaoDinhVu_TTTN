using CaoDinhVu.DAL.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;



using System.ComponentModel.DataAnnotations.Schema;


namespace CaoDinhVu.DAL.Data
{
    public class DBContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        #region DBSet
        public DbSet<AppUser> AppUsers { get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<Option> Options { get; set;}
        public DbSet<Color> Colors { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<OrderDetail> OrderDetails { get; set;}
        public DbSet<Product> Products { get; set;}
        public DbSet<ProductColor> ProductColors { get; set;}
        public DbSet<ProductOption> ProductOptions { get; set;}
        public DbSet<Slider> Sliders { get; set;}
        public DbSet<Brand> Brands { get; set;}
        public DbSet<Image> Images { get; set;}
        public DbSet<Detail> Details { get; set;}
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
