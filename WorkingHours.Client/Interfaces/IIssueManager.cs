using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Client.Interfaces
{
    public interface IIssueManager : IManager
    {
        Task CreateIssueForProjectAsync(int projectId, IssueHeader issue);

        Task UpdateIssueAsync(IssueHeader issue);
    }
}
