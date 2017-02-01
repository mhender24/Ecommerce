using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(Data context) : base(context) { }

    }
}