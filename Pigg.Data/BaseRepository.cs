using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Pigg.Contracts.Repositories;

namespace Pigg.Data
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        ObjectContext _context;
        IObjectSet<T> _objectSet;

        public BaseRepository(IUnitOfWork context)
        {
            if (context == null)            
                throw new ArgumentNullException("context");            
            _context = context as ObjectContext;
        }

        public ObjectContext Context
        {
            get
            {                            
                return _context;
            }
        }

        private IObjectSet<T> ObjectSet
        {
            get
            {
                if (_objectSet == null)
                {
                    _objectSet = this.Context.CreateObjectSet<T>();
                }
                return _objectSet;
            }
        }        

        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public IEnumerable<T> Find(Func<T, bool> where)
        {
            return this.ObjectSet.Where<T>(where);
        }

        public T Single(Func<T, bool> where)
        {
            return this.ObjectSet.Single<T>(where);
        }

        public T First(Func<T, bool> where)
        {
            return this.ObjectSet.First<T>(where);
        }

        public void Delete(T entity)
        {
            this.ObjectSet.DeleteObject(entity);
        }

        public void Add(T entity)
        {
            this.ObjectSet.AddObject(entity);
        }

        public void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
