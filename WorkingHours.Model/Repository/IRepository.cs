using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.Common;

namespace WorkingHours.Model.Repository
{
    public interface IRepository<T> where T: class, IDbEntity
    {
        T GetById(int id, params string[] propsToInclude);

        IPagedList<T> ListPaged(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, OrderInfo<T> orderInfo = null, params string[] propsToInclude);

        IEnumerable<T> List(Expression<Func<T, bool>> filter, OrderInfo<T> orderInfo = null, params string[] propsToInclude);

        void Add(T obj);

        void Update(T obj);

        void Remove(T obj);
    }
}
