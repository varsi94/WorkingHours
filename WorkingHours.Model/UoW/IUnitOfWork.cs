using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.Repository;

namespace WorkingHours.Model.UoW
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IRepository<Project> Projects { get; }

        IRepository<Issue> Issues { get; }

        IWorkTimeRepository WorkTimeLog { get; }

        IRoleRepository Roles { get; }

        void SaveChanges();
    }
}
