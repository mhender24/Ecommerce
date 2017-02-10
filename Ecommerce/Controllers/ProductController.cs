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
        private readonly ProductUnitOfWork _db;

        public ProductController()
        {
            _db = new ProductUnitOfWork();
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
                products = _db.ProductRepository.SearchProduct(searchString);
            else
               products = _db.ProductRepository.Get();

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
            var product = _db.ProductRepository.GetByID(id);
            ViewBag.Categories = _db.CategoryRepository.GetCategoryByProductId(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(_db.SupplierRepository.Get(), "Id", "Name");
            ViewBag.Categories = new MultiSelectList(_db.CategoryRepository.Get(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,Name,Price,Description,ProductImage,CategoryId")] Product product, int[] categoryId)
        {
            if (ModelState.IsValid)
            {
                _db.ProductRepository.Insert(product);
                _db.Save();
                foreach (var catId in categoryId)
                {
                    _db.ProductCategoryRepository.Insert(new ProductCategory { ProductId = product.Id, CategoryId = catId });
                }
                _db.Save();
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
            var product = _db.ProductRepository.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(_db.SupplierRepository.Get(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,Name,Price,Description,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.ProductRepository.Update(product);
                _db.Save();
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
            var product = _db.ProductRepository.GetByID(id);
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
            var product = _db.ProductRepository.GetByID(id);
            _db.ProductRepository.Delete(product);
            _db.Save();
            return RedirectToAction("Index");
        }
    }
}
