using System.Collections.Generic;
using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ecommerce.DataAccessLayer.Data>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(Ecommerce.DataAccessLayer.Data context)
        {
            ApplicationDbContext userContext = new ApplicationDbContext();
            CreateUsers(userContext);
            CreateUserDetails(context, userContext);
            CreateSuppliers(context);
            CreatePayments(context);
            CreateAddresses(context);
            CreateProducts(context);
            CreateCategories(context);
            CreateProductCategories(context);
            CreateOrders(context);
            CreateOrderDetails(context);
        }

        public void CreateOrderDetails(DataAccessLayer.Data context)
        {
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 123.52).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse1").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 123.52).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse4").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 123.52).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse3").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 223.52).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad3").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 223.52).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad1").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 12.52).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad4").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 550.32).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse2").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 550.32).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse1").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 550.32).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse1").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 550.32).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse3").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 74.52).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad1").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 13.52).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad2").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 1235.52).Id,
                    ProductId = context.Products.First(u => u.Name == "mouse4").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 1235.52).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad3").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 226.23).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad1").Id
                },
                new OrderDetail
                {
                    OrderId = context.Orders.First(u => u.Cost == 12.52).Id,
                    ProductId = context.Products.First(u => u.Name == "trackpad2").Id
                },
            };
            orderDetails.ForEach(s => context.OrderDetails.Add(s));
            context.SaveChanges();
        }

        public void CreateOrders(DataAccessLayer.Data context)
        {
            var orders = new List<Order>
            {
                new Order {Cost = 123.52, CustomerId = context.Customers.First(u => u.FirstName == "Marcel").Id},
                new Order {Cost = 223.52, CustomerId = context.Customers.First(u => u.FirstName == "Marcel").Id},
                new Order {Cost = 12.52, CustomerId = context.Customers.First(u => u.FirstName == "Matt").Id},
                new Order {Cost = 550.32, CustomerId = context.Customers.First(u => u.FirstName == "Matt").Id},
                new Order {Cost = 74.52, CustomerId = context.Customers.First(u => u.FirstName == "Matt").Id},
                new Order {Cost = 13.52, CustomerId = context.Customers.First(u => u.FirstName == "Joe").Id},
                new Order {Cost = 12.53, CustomerId = context.Customers.First(u => u.FirstName == "Joe").Id},
                new Order {Cost = 1235.52, CustomerId = context.Customers.First(u => u.FirstName == "Joe").Id},
                new Order {Cost = 226.23, CustomerId = context.Customers.First(u => u.FirstName == "Joe").Id},
            };
            orders.ForEach(s => context.Orders.Add(s));
            context.SaveChanges();
        }

        public void CreateCategories(DataAccessLayer.Data context)
        {
            var categories = new List<Category>
            {
                new Category {Name = "Computer", Keywords = "computer, hardware, cpu"},
                new Category {Name = "Mouse", Keywords = "ergonomic, mouse, hardware"},
                new Category {Name = "Track Pad", Keywords = "track, pad, mouse" }
            };
            categories.ForEach(s => context.Categories.AddOrUpdate(s));
            context.SaveChanges();
        }

        public void CreateProductCategories(DataAccessLayer.Data context)
        {
            var productCategory = new List<ProductCategory>
            {
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "mouse1").Id, CategoryId = context.Categories.First(u => u.Name == "Mouse").Id },
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "mouse2").Id, CategoryId = context.Categories.First(u => u.Name == "Mouse").Id },
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "mouse3").Id, CategoryId = context.Categories.First(u => u.Name == "Mouse").Id },
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "mouse4").Id, CategoryId = context.Categories.First(u => u.Name == "Mouse").Id },
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "trackpad1").Id, CategoryId = context.Categories.First(u => u.Name == "Track Pad").Id },
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "trackpad2").Id, CategoryId = context.Categories.First(u => u.Name == "Track Pad").Id },
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "trackpad3").Id, CategoryId = context.Categories.First(u => u.Name == "Track Pad").Id },
                new ProductCategory {ProductId = context.Products.First(u => u.Name == "trackpad4").Id, CategoryId = context.Categories.First(u => u.Name == "Track Pad").Id }
            };
            productCategory.ForEach(p => context.ProductCategory.AddOrUpdate(p));
            context.SaveChanges();
        }

        public void CreateProducts(DataAccessLayer.Data context)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "mouse1",
                    Price = 100.50,
                    ProductImage = "/image/dog.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "BestBuy").Id
                },
                new Product
                {
                    Name = "mouse2",
                    Price = 20.50,
                    ProductImage = "/image/cat.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "BestBuy").Id

                },
                new Product
                {
                    Name = "mouse3",
                    Price = 50.50,
                    ProductImage = "/image/horse.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "BestBuy").Id
                },
                new Product
                {
                    Name = "mouse4",
                    Price = 3.50,
                    ProductImage = "/image/pig.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "Newegg").Id
                },
                new Product
                {
                    Name = "trackpad1",
                    Price = 40.50,
                    ProductImage =  "/image/chicken.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "Newegg").Id

                },
                new Product
                {
                    Name = "trackpad2",
                    Price = 2348.50,
                    ProductImage = "/image/cow.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "Newegg").Id
                },
                new Product
                {
                    Name = "trackpad3",
                    Price = 35.50,
                    ProductImage = "/image/monkey.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "Dunhams").Id
                },
                new Product
                {
                    Name = "trackpad4",
                    Price = 10.50,
                    ProductImage = "/image/hippo.jpg",
                    SupplierId = context.Suppliers.First(u => u.Name == "Dunhams").Id
                },
            };
            products.ForEach(s => context.Products.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }

        public void CreateAddresses(DataAccessLayer.Data context)
        {
            var addresses = new List<Address>
            {
                new Models.Address
                {
                    Street = "123 A Place",
                    City = "Milan",
                    State = "Michigan",
                    Zipcode = "48160",
                    CustomerId = context.Customers.First(u => u.FirstName == "Marcel").Id
                },
                new Models.Address
                {
                    Street = "123 A Second Place",
                    City = "Orlando",
                    State = "Flordia",
                    Zipcode = "78787",
                    CustomerId = context.Customers.First(u => u.FirstName == "Marcel").Id
                },
                new Models.Address
                {
                    Street = "123 A Awesome Place",
                    City = "Milan",
                    State = "Michigan",
                    Zipcode = "98998",
                    CustomerId = context.Customers.First(u => u.FirstName == "Matt").Id
                },
                new Models.Address
                {
                    Street = "123 A Crappy Place",
                    City = "Orlando",
                    State = "Flordia",
                    Zipcode = "11111",
                    CustomerId = context.Customers.First(u => u.FirstName == "Joe").Id
                }
            };
            addresses.ForEach(s => context.Addresses.AddOrUpdate(p => p.Street, s));
            context.SaveChanges();
        }

        public void CreatePayments(DataAccessLayer.Data context)
        {
            var payments = new List<Payment>
            {
                new Payment
                {
                    CardType = "Visa",
                    CardNumber = "5283428347234",
                    ExpMonth = "3",
                    ExpYear = "2019",
                    BillingAddress = "123 A Place",
                    BillingCity = "Milan",
                    BillingState = "Michigan",
                    BillingZipcode = "48160",
                    CustomerId = context.Customers.First(u => u.FirstName == "Marcel").Id
                },
                new Payment
                {
                    CardType = "Master",
                    CardNumber = "28349283948234",
                    ExpMonth = "9",
                    ExpYear = "2019",
                    BillingAddress = "123 A Place",
                    BillingCity = "Milan",
                    BillingState = "Michigan",
                    BillingZipcode = "48160",
                    CustomerId = context.Customers.First(u => u.FirstName == "Marcel").Id
                },
                new Payment
                {
                    CardType = "Visa",
                    CardNumber = "75675354345",
                    ExpMonth = "2",
                    ExpYear = "2019",
                    BillingAddress = "123 Another Place",
                    BillingCity = "Brighton",
                    BillingState = "Michigan",
                    BillingZipcode = "78787",
                    CustomerId = context.Customers.First(u => u.FirstName == "Matt").Id
                },
                new Payment
                {
                    CardType = "Visa",
                    CardNumber = "62534452344",
                    ExpMonth = "7",
                    ExpYear = "2017",
                    BillingAddress = "123 Final Place",
                    BillingCity = "Birmingham",
                    BillingState = "Alabama",
                    BillingZipcode = "98765",
                    CustomerId = context.Customers.First(u => u.FirstName == "Joe").Id
                },

            };
            payments.ForEach(s => context.Payments.AddOrUpdate(p => p.CardNumber, s));
            context.SaveChanges();
        }

        public void CreateSuppliers(DataAccessLayer.Data context)
        {
            var suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    Name = "Newegg",
                    Phone = "123-123-1234",
                    Address = "123 A Place",
                    City = "Seattle",
                    State = "WA",
                    Zipcode = "64532"
                },
                new Supplier()
                {
                    Name = "Dunhams",
                    Phone = "123-123-4234",
                    Address = "123 Some Place",
                    City = "Seattle",
                    State = "WA",
                    Zipcode = "64532"
                },
                new Supplier()
                {
                    Name = "BestBuy",
                    Phone = "123-585-4234",
                    Address = "123 Another Place",
                    City = "New Orleans",
                    State = "Georgia",
                    Zipcode = "65874"
                },

            };

            suppliers.ForEach(s => context.Suppliers.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }

        public void CreateUserDetails(DataAccessLayer.Data context, Models.ApplicationDbContext userContext)
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Marcel",
                    LastName = "Henderson",
                    Phone = "734-555-8282",
                    UserId = userContext.Users.First(u => u.UserName == "admin@gmail.com").Id
                },
                new Customer
                {
                    FirstName = "Matt",
                    LastName = "Bolin",
                    Phone = "734-555-1212",
                    UserId = userContext.Users.First(u => u.UserName == "manager@gmail.com").Id
                },
                new Customer
                {
                    FirstName = "Joe",
                    LastName = "Schmo",
                    Phone = "734-555-6343",
                    UserId = userContext.Users.First(u => u.UserName == "user@gmail.com").Id
                }
            };
            customers.ForEach(s => context.Customers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();
        }

        public void CreateUsers(Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };

                manager.Create(user, "P@ssw0rd");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Manager" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "manager@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "manager@gmail.com", Email = "manager@gmail.com" };

                manager.Create(user, "P@ssw0rd");
                manager.AddToRole(user.Id, "Manager");
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "user@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "user@gmail.com", Email = "user@gmail.com" };

                manager.Create(user, "P@ssw0rd");
                manager.AddToRole(user.Id, "User");
            }
        }
    }
}