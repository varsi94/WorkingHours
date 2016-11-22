using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Client.Interfaces
{
    public interface IProjectManager : IManager
    {
        Task CreateAsync(ProjectHeader projectHeader);

        Task UpdateAsync(ProjectHeader projectHeader);

        Task<List<ProjectHeader>> GetMyProjectsAsync();

        Task<ProjectInfo> GetProjectAsync(int id);

        Task AddMembersToProjectAsync(int projectId, Dictionary<int, Roles> membersToAdd);

        Task RemoveMemberFromProjectAsync(int projectId, int memberToRemove);

        Task<byte[]> GetReportAsync(int projectId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
