using Ecommerce.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class Data : DbContext
    {

            public DbSet<Address> Addresses { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderDetail> OrderDetails { get; set; }
            public DbSet<Payment> Payments { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductDetail> ProductDetails { get; set; }
            public DbSet<Supplier> Suppliers { get; set; }
            public DbSet<ApplicationUser> Users { get; set; }
            public DbSet<IdentityRole> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
                modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
                modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser", "Data");
            modelBuilder.Entity<IdentityRole>().ToTable("IdentityRole", "Data");


        }
    }
}