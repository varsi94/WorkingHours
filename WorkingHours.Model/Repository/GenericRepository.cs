using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using WorkingHours.Model.Common;
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

        public virtual IPagedList<T> List(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, OrderInfo<T> orderInfo = null, params string[] propsToInclude)
        {
            var query = DbContext.Set<T>().AsQueryable();
            foreach (var expression in propsToInclude)
            {
                query = query.Include(expression);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderInfo != null)
            {
                if (orderInfo.Direction == SortDirection.Ascending)
                {
                    query = query.OrderBy(orderInfo.OrderBy);
                }
                else if (orderInfo.Direction == SortDirection.Descending)
                {
                    query = query.OrderByDescending(orderInfo.OrderBy);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return query.ToPagedList(pageIndex, pageSize);
        }
    }
}
