using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Dto;
using WorkingHours.Model;

namespace WorkingHours.Bll.Interfaces
{
    public interface IProjectManager
    {
        void Add(Project project, int managerId);

        List<ProjectHeader> List(int userId);
    }
}
