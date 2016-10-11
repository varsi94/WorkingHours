using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.UoW;

namespace WorkingHours.Bll.Managers
{
    public class ManagerBase
    {
        protected IUnitOfWork UoW { get; }

        public ManagerBase(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }
    }
}
