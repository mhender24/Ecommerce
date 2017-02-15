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
        TestDbSet<Supplier> context;
        SupplierController supplierController;

        [OneTimeSetUp]
        public void TestSetup()
        {
            context = new TestDbSet<Supplier>();
            supplierController = new SupplierController(context);
        }

        [SetUp]
        public void SetUp()
        {
            context.Clear();
        }

        [Test]
        public void Supplier_DefaultIndexMethod_ReturnsAllSuppliers()
        {
            var result = supplierController.Index() as ViewResult;
            Assert.IsEmpty(result.ViewName);
        }

        [Test]
        public void Supplier_DetailTakesNullableId_ReturnsSingleSupplierAtId()
        {
            context.Insert(GetMockSupplier());
            var result = supplierController.Details(0) as ViewResult;
            Assert.IsEmpty(result.ViewName);
        }

        [Test]
        public void Supplier_DetailWithNoIdPassed_HttpStatusBadRequest()
        {
            var result = supplierController.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Supplier_DetailWithNegativeId_ArgumentOutOfRangeException()
        {
            var result = Assert.Throws<ArgumentOutOfRangeException>(() => supplierController.Details(-1));
            Assert.IsNotNull(result.Message);
        }

        [Test]
        public void Supplier_CreateNewSupplierValid_ContextCountOne()
        {
            var context = new TestDbSet<Supplier>();
            var controller = new SupplierController(context);
            var result = controller.Create(GetMockSupplier());
            Assert.AreEqual(1, context.Count());
        }

        [Test]
        public void Supplier_CreateNewSupplierInvalid_ContextCountZero()
        {
            supplierController.ModelState.AddModelError("Test", "Test");
            var result = supplierController.Create(GetMockSupplier());
            Assert.AreEqual(0, context.Count());
        }

        [Test]
        public void Supplier_EditHttpGetValid_ReturnSingleSupplier()
        {
            context.Insert(GetMockSupplier());
            var result = supplierController.Edit(0) as ViewResult;
            var actual = result.Model as Supplier;
            Assert.AreEqual(actual.Id, GetMockSupplier().Id);
        }

        [Test]
        public void Supplier_EditHttpGetNoSupplierAtId_ArgumentOutOfRangeException()
        {
            context.Insert(GetMockSupplier());
            var result = Assert.Throws<ArgumentOutOfRangeException>(() => supplierController.Details(3));
            Assert.IsNotNull(result.Message);
        }

        [Test]
        public void Supplier_EditNewSupplierInvalidModelState_ContextCountZero()
        {
            supplierController.ModelState.AddModelError("Test", "Test");
            var result = supplierController.Edit(GetMockSupplier());
            Assert.AreEqual(0, context.Count());
        }

        [Test]
        public void Supplier_EditSupplier_ContextChanged()
        {
            context.Insert(GetMockSupplier());
            Supplier test = new Supplier { Id = 1, Name = "Mock", Phone = "555-555-5555", Address = "555 Someplace", City = "Milan", State = "Michingan", Zipcode = "48168" };
            var result = supplierController.Edit(test);
            Assert.AreNotEqual(context.GetByID(0).City, test.City);
        }

        [Test]
        public void Supplier_DeleteSupplierNoIdPassed_BadRequest()
        {
            var result = supplierController.Delete(null) as HttpStatusCodeResult;
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Supplier_DeleteSupplierNotFound_ArgumentOutOfRangeException()
        {
            var result = Assert.Throws<ArgumentOutOfRangeException>(() => supplierController.Delete(3));
            Assert.IsNotNull(result.Message);
        }

        [Test]
        public void Supplier_DeleteSupplierIdFound_ReturnsSingleSupplier()
        {
            context.Insert(GetMockSupplier());
            var result = supplierController.Delete(0) as ViewResult;
            var actual = result.Model as Supplier;
            Assert.AreEqual(actual.Id, GetMockSupplier().Id);
        }

        [Test]
        public void Supplier_DeleteConfirmSupplierIdFound_SupplierRemoved()
        {
            context.Insert(GetMockSupplier());
            var result = supplierController.DeleteConfirmed(0);
            Assert.AreEqual(0, context.Count());
        }

        public Supplier GetMockSupplier()
        {
            return new Supplier { Id = 1, Name = "Mock", Phone = "555-555-5555", Address = "555 Someplace", City = "Saline", State = "Michingan", Zipcode = "48168" };
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            context = null;
            supplierController = null;
        }
    }
}
