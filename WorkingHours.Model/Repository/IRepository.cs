using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.Repository
{
    public interface IRepository<T> where T: class, IDbEntity
    {
        T GetById(int id);

        void Add(T obj);

        void Update(T obj);

        void Remove(T obj);
    }
}
