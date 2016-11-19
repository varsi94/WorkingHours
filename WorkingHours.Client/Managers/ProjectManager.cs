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
using WorkingHours.Shared.Model;

namespace WorkingHours.Client.Managers
{
    public class ProjectManager : ManagerBase, IProjectManager
    {
        public ProjectManager(IAppSettingsManager configurationManager) : base(configurationManager)
        {
        }

        public async Task AddMembersToProjectAsync(int projectId, Dictionary<int, Roles> membersToAdd)
        {
            if (LoginInfo.Role != Roles.Manager)
            {
                throw new UnauthorizedAccessException();
            }

            if (membersToAdd.ContainsKey(LoginInfo.Id.Value))
            {
                throw new InvalidOperationException();
            }

            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PostAsJsonAsync($"api/project/{projectId}/membersAdd", membersToAdd);
                if (!httpResult.IsSuccessStatusCode)
                {
                    var messageResult = await httpResult.Content.ReadAsAsync<ErrorMessage>();
                    if (httpResult.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        throw new ServerException(messageResult.Message);
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new ServerException(messageResult.Message);
                    }
                }
            }
        }

        public async Task CreateAsync(ProjectHeader projectHeader)
        {
            if (LoginInfo.Role != Roles.Manager)
            {
                throw new UnauthorizedAccessException();
            }

            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PostAsJsonAsync("api/projects/create", projectHeader);
                if (!httpResult.IsSuccessStatusCode)
                {
                    if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var modelState = await httpResult.Content.ReadAsAsync<ModelState>();
                        throw new ModelStateException("Validation failed for this project!", modelState);
                    }
                    throw new ServerException("Internal server error occured!");
                }
            }
        }

        public async Task<List<ProjectHeader>> GetMyProjectsAsync()
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.GetAsync("api/projects");
                if (httpResult.IsSuccessStatusCode)
                {
                    return await httpResult.Content.ReadAsAsync<List<ProjectHeader>>();
                }
                else
                {
                    throw new ServerException("Internal server error occured!");
                }
            }
        }

        public async Task<ProjectInfo> GetProjectAsync(int id)
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.GetAsync($"api/project/{id}");
                if (httpResult.IsSuccessStatusCode)
                {
                    return await httpResult.Content.ReadAsAsync<ProjectInfo>();
                }
                else
                {
                    if (httpResult.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException("Project not found!");
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    throw new ServerException("Internal server error occured!");
                }
            }
        }

        public async Task<byte[]> GetReportAsync(int projectId, DateTime? startDate = default(DateTime?), DateTime? endDate = default(DateTime?))
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult =
                    await client.GetAsync($"api/project/report/{projectId}/?startDate={startDate}&endDate={endDate}");

                if (httpResult.IsSuccessStatusCode)
                {
                    return await httpResult.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    if (httpResult.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException("Project not found!");
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
