using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.Repository;

namespace WorkingHours.Model.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        void SaveChanges();
    }
}
