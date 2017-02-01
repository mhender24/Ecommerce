using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(Data context) : base(context) { }

    }
}