﻿using System;
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
                        throw new UnauthorizedAccessException();
                    }
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
    }
}
