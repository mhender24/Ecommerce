using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(Data context) : base(context) { }

    }
}