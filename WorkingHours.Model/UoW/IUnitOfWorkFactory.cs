using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.UoW
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUoW();
    }
}
