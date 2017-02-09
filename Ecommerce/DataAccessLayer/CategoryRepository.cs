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

    }
}