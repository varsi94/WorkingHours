using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Common;
using WorkingHours.Client.Exceptions;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Client.Managers
{
    public class IssueManager : ManagerBase, IIssueManager
    {
        public IssueManager(IAppSettingsManager configurationManager) : base(configurationManager)
        {
        }

        public async Task CreateIssueForProjectAsync(int projectId, IssueHeader issue)
        {
            if (LoginInfo.Role != Shared.Model.Roles.Manager)
            {
                throw new UnauthorizedAccessException();
            }

            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PostAsJsonAsync($"api/issues/create/{projectId}", issue);
                if (!httpResult.IsSuccessStatusCode)
                {
                    if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var modelState = await httpResult.Content.ReadAsAsync<ModelState>();
                        throw new ModelStateException("Validation failed for this issue!", modelState);
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException("Project not found!");
                    }
                }
            }
        }

        public async Task UpdateIssueAsync(IssueHeader issue)
        {
            if (LoginInfo.Role != Shared.Model.Roles.Manager)
            {
                throw new UnauthorizedAccessException();
            }

            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PutAsJsonAsync("api/issue", issue);
                if (!httpResult.IsSuccessStatusCode)
                {
                    if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new ModelStateException("Model is not valid!",
                            await httpResult.Content.ReadAsAsync<ModelState>());
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException("Issue not found!");
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Conflict)
                    {
                        throw new ServerException("Conflicting edit!");
                    }
                }
            }
        }
    }
}
