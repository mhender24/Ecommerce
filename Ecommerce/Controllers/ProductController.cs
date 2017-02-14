using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DataAccessLayer;
using Ecommerce.Models;
using Ecommerce.CustomFilters;
using PagedList;

namespace Ecommerce.Controllers
{
    [AuthLog(Roles = "Admin,Manager")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;

        public ProductController(IUnitOfWork context)
        {
            _db = context;
        }

        [AllowAnonymous]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";
            IEnumerable<Product> products;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            if (!String.IsNullOrEmpty(searchString))
                products = _db.Repository<Product>().Get(p => p.Name.Contains(searchString.ToUpper()) || p.Description.Contains(searchString.ToUpper()));
            else
                products = _db.Repository<Product>().Get();

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:  // Name ascending 
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product =  _db.Repository<Product>().Get(p => p.Id == id);
            ViewBag.Categories = _db.Repository<Category>().Get();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.ElementAt(0));
        }

        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(_db.Repository<Supplier>().Get(), "Id", "Name");
            ViewBag.Categories = new MultiSelectList(_db.Repository<Category>().Get(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,Name,Price,Description,ProductImage,CategoryId")] Product product, int[] categoryId)
        {
            if (ModelState.IsValid)
            {
                _db.Repository<Product>().Insert(product);
                _db.Repository<Product>().Save();
                foreach (var catId in categoryId)
                {
                    _db.Repository<ProductCategory>().Insert(new ProductCategory { ProductId = product.Id, CategoryId = catId });
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Repository<Product>().GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(_db.Repository<Supplier>().Get(), "Id", "Name");
            ViewBag.Categories = new MultiSelectList(_db.Repository<Category>().Get(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,Name,Price,Description,ProductImage")] Product product, int[] categoryId)
        {
            if (ModelState.IsValid)
            {
                _db.Repository<Product>().Update(product);
                string sql = "Select * From ProductCategory WHERE ProductId = @p0";
                var productCategory = _db.Repository<ProductCategory>().GetWithRawSql(sql, product.Id);

                foreach (var prodCat in productCategory)
                    _db.Repository<ProductCategory>().Delete(prodCat);
                foreach (var catId in categoryId)
                    _db.Repository<ProductCategory>().Insert(new ProductCategory { ProductId = product.Id, CategoryId = catId });

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Repository<Product>().GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _db.Repository<Product>().GetByID(id);
            _db.Repository<Product>().Delete(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
