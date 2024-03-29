﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.Interfaces
{
    public interface IWorkTimeManager
    {
        void AddWorkItem(int issueId, int userId, WorkTimeDto workTime);

        PagedResult<WorkTimeDto> GetMyWorkTimes(int userId, int issueId, PagingInfo pagingInfo);

        PagedResult<ManagerWorkTimeDto> GetWorkTimesForManager(int userId, int issueId, PagingInfo pagingInfo);

        void UpdateWorkTime(int userId, UpdateWorkTimeDto workTime);
        
        void DeleteWorkTime(int userId, int workTimeId);
    }
}
