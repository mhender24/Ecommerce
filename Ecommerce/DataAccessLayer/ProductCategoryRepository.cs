using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>
    {
        public ProductCategoryRepository(Data context) : base(context) { }

        public IEnumerable<ProductCategory> allRecordsWithProductId(int productId)
        {
            return from pc in context.ProductCategory
                   where pc.ProductId == productId
                   select pc;
        }

    }
}