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

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly Data _db = new Data();

        public ActionResult Index()
        {
            var products = _db.Products.Include(p => p.ProductDetail).Include(p => p.Supplier);
            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.ProductDetailId = new SelectList(_db.ProductDetails, "Id", "Description");
            ViewBag.SupplierId = new SelectList(_db.Suppliers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,ProductDetailId,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductDetailId = new SelectList(_db.ProductDetails, "Id", "Description", product.ProductDetailId);
            ViewBag.SupplierId = new SelectList(_db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductDetailId = new SelectList(_db.ProductDetails, "Id", "Description", product.ProductDetailId);
            ViewBag.SupplierId = new SelectList(_db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,ProductDetailId,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductDetailId = new SelectList(_db.ProductDetails, "Id", "Description", product.ProductDetailId);
            ViewBag.SupplierId = new SelectList(_db.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _db.Products.Find(id);
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
            var product = _db.Products.Find(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
