using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Model.UoW
{
    public class UoWFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork GetUoW()
        {
            return new UnitOfWork(new AppDbContext());
        }
    }
}
