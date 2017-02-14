using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        //T Repository<T>(T t, int MasterID) where T : class;
        IRepo<T> Repository<T>() where T : class;
    }
}