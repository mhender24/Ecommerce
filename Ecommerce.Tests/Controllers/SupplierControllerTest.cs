using Ecommerce.Controllers;
using Ecommerce.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            context.Insert(GetMockSupplier());
            var result = supplierController.Details(0) as ViewResult;
            Assert.IsEmpty(result.ViewName);
        }

        public Supplier GetMockSupplier()
        {
            return new Supplier { Id = 1, Name = "Mock", Phone = "555-555-5555", Address = "555 Someplace", City = "Saline", State = "Michingan", Zipcode = "48168" };
        }
    }
}
