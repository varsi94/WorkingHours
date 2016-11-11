using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Client.Interfaces
{
    public interface IWorkTimeManager : IManager
    {
        Task AddWorkTimeAsync(int issueId, WorkTimeDto workTime);

        Task<PagedResult<WorkTimeDto>> GetMyWorkTimesAsync(int issueId, int pageSize, int pageIndex);
    }
}
