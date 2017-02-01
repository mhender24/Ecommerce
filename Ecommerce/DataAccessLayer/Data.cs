using Ecommerce.Models;
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

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
    }
}