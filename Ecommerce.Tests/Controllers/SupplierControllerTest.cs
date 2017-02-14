using Ecommerce.Controllers;
using Ecommerce.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ecommerce.Tests.Controllers
{
    [TestFixture]
    class SupplierControllerTest
    {
        [Test]
        public void Supplier_DefaultIndexMethod_ReturnsAllSuppliers()
        {
            var supplierController = new SupplierController(new TestDbSet<Supplier>());
            var result = supplierController.Index() as ViewResult;
            Assert.IsEmpty(result.ViewName);
        }

        [Test]
        public void Supplier_DetailTakesNullableId_ReturnsSingleSupplierAtId()
        {
            var context = new TestDbSet<Supplier>();
            var supplierController = new SupplierController(context);
            context.Insert(GetMockValidSupplier());
            var result = supplierController.Details(0) as ViewResult;
            Assert.IsEmpty(result.ViewName);
        }

        [Test]
        public void Supplier_DetailWithNoIdPassed_HttpStatusBadRequest()
        {
            var context = new TestDbSet<Supplier>();
            var supplierController = new SupplierController(context);
            var result = supplierController.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Supplier_DetailWithNegativeId_ArgumentOutOfRangeException()
        {
            var context = new TestDbSet<Supplier>();
            var supplierController = new SupplierController(context);
            var result = Assert.Throws<ArgumentOutOfRangeException>(() => supplierController.Details(-1));
            Assert.IsNotNull(result.Message);
        }

        [Test]
        public void Supplier_CreateNewSupplierValid_ContextCountOne()
        {
            var context = new TestDbSet<Supplier>();
            var supplierController = new SupplierController(context);
            var result = supplierController.Create(GetMockValidSupplier());
            Assert.AreEqual(1, context.Count());
        }

        [Test]
        public void Supplier_CreateNewSupplierInvalid_ContextCountOne()
        {
            var context = new TestDbSet<Supplier>();
            var supplierController = new SupplierController(context);
            supplierController.ModelState.AddModelError("Test", "Test");
            var result = supplierController.Create(GetMockInvalidSupplier());
            Assert.AreEqual(0, context.Count());
        }

        [Test]
        public void Supplier_EditHttpGetValid_ReturnSingleSupplier()
        {
            var context = new TestDbSet<Supplier>();
            var supplierController = new SupplierController(context);
            context.Insert(GetMockValidSupplier());
            var result = supplierController.Edit(0) as ViewResult;
            var x = result.Model as Supplier;
            var expected = GetMockValidSupplier();
            Assert.AreEqual(result, expected);
        }

        public Supplier GetMockValidSupplier()
        {
            return new Supplier { Id = 1, Name = "Mock", Phone = "555-555-5555", Address = "555 Someplace", City = "Saline", State = "Michingan", Zipcode = "48168" };
        }

        public Supplier GetMockInvalidSupplier()
        {
            return new Supplier { Id = 1, Phone = "555-555-5555", Address = "555 Someplace", City = "Saline", State = "Michingan", Zipcode = "48168" };
        }
    }
}
