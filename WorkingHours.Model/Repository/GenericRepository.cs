using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Model.Repository
{
    internal class GenericRepository<T> : IRepository<T> where T: class, IDbEntity
    {
        protected AppDbContext DbContext { get; }

        public GenericRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Add(T obj)
        {
            DbContext.Set<T>().Add(obj);
        }

        public virtual T GetById(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public virtual void Remove(T obj)
        {
            if (DbContext.Entry(obj).State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(obj);
            }
            DbContext.Set<T>().Remove(obj);
        }

        public virtual void Update(T obj)
        {
            if (DbContext.Entry(obj).State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(obj);
            }
            DbContext.Entry(obj).State = EntityState.Modified;
        }
    }
}
