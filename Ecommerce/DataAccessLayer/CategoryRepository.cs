using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(Data context) : base(context) { }

        public IEnumerable<Category> GetCategoryByProductId(int? productId)
        {
            
            return from pc in context.ProductCategory
                   join c in context.Categories
                   on pc.CategoryId equals c.Id
                   where pc.ProductId == productId
                   select c;
        }

    }
}