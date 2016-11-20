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
    public class WorkTimeManager : ManagerBase, IWorkTimeManager
    {
        public WorkTimeManager(IAppSettingsManager configurationManager) : base(configurationManager)
        {
        }

        public async Task AddWorkTimeAsync(int issueId, WorkTimeDto workTime)
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PostAsJsonAsync($"/api/worktime/create/{issueId}", workTime);
                if (!httpResult.IsSuccessStatusCode)
                {
                    var msg = await httpResult.Content.ReadAsAsync<ErrorMessage>();
                    if (httpResult.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new InvalidOperationException(msg.Message);
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new ModelStateException("Model is not valid!",
                            await httpResult.Content.ReadAsAsync<ModelState>());
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException(msg.Message);
                    }
                }
            }
        }

        public async Task DeleteWorkTimeAsync(int workTimeId)
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.DeleteAsync($"/api/worktime/{workTimeId}");
                var msg = await httpResult.Content.ReadAsAsync<ErrorMessage>();
                if (httpResult.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ServerException("Worktime not found!");
                }
                else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(msg.Message);
                }
                else if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ServerException(msg.Message);
                }
            }
        }

        public async Task<PagedResult<WorkTimeDto>> GetMyWorkTimesAsync(int issueId, int pageSize, int pageIndex)
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.GetAsync($"/api/worktimes/{issueId}/?pageSize={pageSize}&pageIndex={pageIndex}");
                if (httpResult.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ServerException("Issue not found!");
                }

                if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }

                return await httpResult.Content.ReadAsAsync<PagedResult<WorkTimeDto>>();
            }
        }

        public async Task<PagedResult<ManagerWorkTimeDto>> GetWorkTimesForManagerAsync(int issueId, int pageSize, int pageIndex)
        {
            if (LoginInfo.Role != Roles.Manager)
            {
                throw new UnauthorizedAccessException();
            }

            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.GetAsync($"/api/worktimes/manager/{issueId}/?pageSize={pageSize}&pageIndex={pageIndex}");
                if (httpResult.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ServerException("Issue not found!");
                }

                if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }

                return await httpResult.Content.ReadAsAsync<PagedResult<ManagerWorkTimeDto>>();
            }
        }

        public async Task UpdateWorkTimeAsync(UpdateWorkTimeDto workTime)
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PutAsJsonAsync("/api/worktime", workTime);
                if (httpResult.IsSuccessStatusCode)
                {
                    return;
                }
                var msg = await httpResult.Content.ReadAsAsync<ErrorMessage>();
                if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(msg.Message);
                }
                else if (httpResult.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new ServerException(msg.Message);
                }
                else if (httpResult.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ServerException(msg.Message);
                }
                else if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new InvalidOperationException(msg.Message);
                }
            }
        }
    }
}
