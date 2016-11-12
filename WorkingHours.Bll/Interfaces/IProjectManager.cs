using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.Interfaces
{
    public interface IProjectManager
    {
        int Add(Project project, int managerId);

        List<ProjectHeader> List(int userId);

        ProjectInfo GetProjectInfo(int projectId, int userId);

        void AddMembersToProject(int projectId, int managerId, Dictionary<int, Roles> users);

        void RemoveMembersFromProject(int projectId, int managerId, List<int> userIds);

        byte[] GetReport(int userId, int projectId, DateTime? startDate, DateTime? endDate);
    }
}
