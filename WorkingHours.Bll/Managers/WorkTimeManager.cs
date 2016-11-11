using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Exceptions;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.Common;
using WorkingHours.Model.Exceptions;
using WorkingHours.Model.UoW;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.Managers
{
    public class WorkTimeManager : ManagerBase, IWorkTimeManager
    {
        public WorkTimeManager(IUnitOfWork UoW) : base(UoW)
        {
        }

        public void AddWorkItem(int issueId, int userId, WorkTimeDto workTime)
        {
            var dummy = new Issue();
            var issue = UoW.Issues.GetById(issueId, nameof(dummy.Project), nameof(dummy.Project) + "." + nameof(dummy.Project.AssociatedMembers));

            if (issue == null)
            {
                throw new NotFoundException("Issue not found!");
            }

            if (!issue.Project.AssociatedMembers.Any(x => x.UserId == userId && x.IsActive))
            {
                throw new UnauthorizedException("You are not associated to this project!");
            }

            if (issue.Project.IsClosed || issue.IsClosed)
            {
                throw new UnauthorizedException("Project or issue is already closed!");
            }

            var workTimeObj = Mapper.Map<WorkTime>(workTime);
            workTimeObj.EmployeeId = userId;
            workTimeObj.IssueId = issue.Id;
            UoW.WorkTimeLog.Add(workTimeObj);
            UoW.SaveChanges();
        }

        public PagedResult<WorkTimeDto> GetMyWorkTimes(int userId, int issueId, PagingInfo pagingInfo)
        {
            var dummy = new Issue();
            var issue = UoW.Issues.GetById(issueId, nameof(dummy.Project) + "." + nameof(dummy.Project.AssociatedMembers));
            if (issue == null)
            {
                throw new NotFoundException("Issue not found!");
            }

            if (!issue.Project.AssociatedMembers.Any(x => x.UserId == userId))
            {
                throw new UnauthorizedException("You are not associated to this project!");
            }

            var orderInfo = new OrderInfo<WorkTime, DateTime> {Direction = SortDirection.Descending, OrderBy = x => x.Date};
            var list = UoW.WorkTimeLog.ListPaged(x => x.EmployeeId == userId && x.IssueId == issueId,
                pagingInfo.PageIndex, pagingInfo.PageSize, orderInfo);
            return Mapper.Map<PagedResult<WorkTimeDto>>(list);
        }

        public PagedResult<WorkTimeDto> GetWorkTimesForAdmin(int userId, int issueId, PagingInfo pagingInfo)
        {
            throw new NotImplementedException();
        }
    }
}
