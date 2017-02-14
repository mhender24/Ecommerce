using Ecommerce.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class GenericUnitOfWork : IDisposable, IUnitOfWork
    {
        private Data context = null;
        private bool disposed = false;

        public GenericUnitOfWork()
        {
            context = new Data();
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepo<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepo<T>;
            }
            IRepo<T> repo = new BaseRepository<T>(context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}