using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Client.Interfaces
{
    public interface IProjectManager
    {
        Task CreateAsync(ProjectHeader projectHeader);

        Task<List<ProjectHeader>> GetMyProjectsAsync();

        Task<ProjectInfo> GetProjectAsync(int id);
    }
}
