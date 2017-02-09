using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class ProductRepository : BaseRepository<Product>, IDisposable
    {
        public ProductRepository(Data context) : base(context) { }

        public IEnumerable<Product> SearchProduct(string filter)
        {
            return (from p in context.Products
                    where p.Name.ToUpper().Contains(filter.ToUpper()) ||
                         p.ProductDetail.Description.ToUpper().Contains(filter.ToUpper())
                    select p
                   );
        }
    }
}