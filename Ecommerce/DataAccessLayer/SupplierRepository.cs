﻿using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class SupplierRepository : BaseRepository<Supplier>, IDisposable
    {
        public SupplierRepository(Data context) : base(context) { }
    }
}