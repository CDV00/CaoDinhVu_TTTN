﻿using CaoDinhVu.WEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaoDinhVu.WEB.Data
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
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
