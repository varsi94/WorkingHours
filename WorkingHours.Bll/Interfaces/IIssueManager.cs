using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;

namespace WorkingHours.Bll.Interfaces
{
    public interface IIssueManager
    {
        void AddIssueToProject(int projectId, Issue issue, int managerId);
    }
}
