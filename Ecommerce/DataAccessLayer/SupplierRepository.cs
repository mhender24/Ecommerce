using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class SupplierRepository : BaseRepository<Supplier>, IDisposable
    {
        public SupplierRepository(Data context) : base(context) { }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}