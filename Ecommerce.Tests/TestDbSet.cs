using Ecommerce.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Ecommerce.Tests
{
    public class TestDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T>, IRepo<T>
         where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public void Insert(T item)
        {
            _data.Add(item);
        }


        public void Delete(T item)
        {
            _data.Remove(item);
        }

        public void Delete(int? id)
        {
            _data.RemoveAt((int)id);
        }

        public override T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(_data); }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return _data;
        }

        public T GetByID(int? id)
        {
            if (_data[(int)id] != null)
                return _data[(int)id];
            return null;
        }

        public void Update(T entityToUpdate)
        {
            for(int i=0; i<_data.Count(); i++)
            {
                if (_data[i].Equals(entityToUpdate))
                    _data[i] = entityToUpdate;
            }
        }

        public void Save()
        {
            
        }

        public IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
