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

namespace Ecommerce.Controllers
{
    [AuthLog(Roles = "Admin, Manager")]
    public class SupplierController : Controller
    {
        private readonly SupplierRepository _db;

        public SupplierController()
        {
            _db = new SupplierRepository(new Data());
        }

        public ActionResult Index()
        {
            return View(_db.Get());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _db.GetByID(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Phone,Address,City,State,Zipcode")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _db.Insert(supplier);
                _db.Save();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = _db.GetByID(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Address,City,State,Zipcode")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _db.Update(supplier);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = _db.GetByID(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var supplier = _db.GetByID(id);
            _db.Delete(supplier);
            _db.Save();
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
