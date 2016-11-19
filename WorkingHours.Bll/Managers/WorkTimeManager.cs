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
        private readonly ITimeService timeService;
        private readonly IServerConfigurationManager configurationManager;

        public WorkTimeManager(IUnitOfWork UoW, ITimeService timeService, IServerConfigurationManager configurationManager) : base(UoW)
        {
            this.timeService = timeService;
            this.configurationManager = configurationManager;
        }

        public void AddWorkItem(int issueId, int userId, WorkTimeDto workTime)
        {
            var dummy = new Issue();
            var issue = UoW.Issues.GetById(issueId, nameof(dummy.Project), nameof(dummy.Project) + "." + nameof(dummy.Project.AssociatedMembers));

            if (issue == null)
            {
                throw new NotFoundException("Issue not found!");
            }

            if (!issue.Project.AssociatedMembers.Any(x => x.UserId == userId))
            {
                throw new UnauthorizedException("You are not associated to this project!");
            }

            if (issue.Project.AssociatedMembers.Any(x => x.UserId == userId && !x.IsActive))
            {
                throw new UnauthorizedException("You are inactive in this project!");
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

        public void DeleteWorkTime(int userId, int workTimeId)
        {
            var dummy = new WorkTime();
            var workTime = UoW.WorkTimeLog.GetById(workTimeId, nameof(dummy.Issue) + "." + nameof(dummy.Issue.Project) + "." + nameof(dummy.Issue.Project.AssociatedMembers));
            if (workTime == null)
            {
                throw new NotFoundException("Worktime not found!");
            }

            if (workTime.EmployeeId != userId)
            {
                throw new UnauthorizedException("Worktime is not yours!");
            }

            if (workTime.Issue.Project.AssociatedMembers.Any(x => x.UserId == userId && !x.IsActive))
            {
                throw new UnauthorizedException("You are inactive in this project!");
            }

            if ((timeService.Now - workTime.Date).TotalDays > configurationManager.WorkTimeUpdateIntervalInDays)
            {
                throw new InvalidOperationException();
            }

            if (workTime.Issue.IsClosed || workTime.Issue.Project.IsClosed)
            {
                throw new UnauthorizedException("Issue or project is closed!");
            }

            UoW.WorkTimeLog.Remove(workTime);
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
            var result = Mapper.Map<PagedResult<WorkTimeDto>>(list);
            foreach (var workTime in result.Items)
            {
                workTime.CanUpdate = (timeService.Now - workTime.Date).TotalDays < configurationManager.WorkTimeUpdateIntervalInDays;
            }
            return result;
        }

        public PagedResult<ManagerWorkTimeDto> GetWorkTimesForManager(int userId, int issueId, PagingInfo pagingInfo)
        {
            var dummy = new Issue();
            var issue = UoW.Issues.GetById(issueId, nameof(dummy.Project) + "." + nameof(dummy.Project.AssociatedMembers));
            if (issue == null)
            {
                throw new NotFoundException("Issue not found!");
            }

            var managerRole = UoW.Roles.GetRole(Roles.Manager);
            if (!issue.Project.AssociatedMembers.Any(x => x.UserId == userId && x.RoleId == managerRole.Id))
            {
                throw new UnauthorizedException("You are not associated to this project as manager!");
            }

            var orderInfo = new OrderInfo<WorkTime, DateTime> { Direction = SortDirection.Descending, OrderBy = x => x.Date };
            var dummyWorkTime = new WorkTime();
            var list = UoW.WorkTimeLog.ListPaged(x => x.IssueId == issueId, pagingInfo.PageIndex, pagingInfo.PageSize,
                orderInfo, nameof(dummyWorkTime.Employee));
            var result = Mapper.Map<PagedResult<ManagerWorkTimeDto>>(list);
            foreach (var managerWorkTimeDto in result.Items)
            {
                var userProject = issue.Project.AssociatedMembers.Single(x => x.UserId == managerWorkTimeDto.Employee.Id);
                managerWorkTimeDto.Employee.RoleInProjectEnum =
                    (userProject.RoleId == managerRole.Id)
                        ? Shared.Model.Roles.Manager
                        : Shared.Model.Roles.Employee;

                managerWorkTimeDto.Employee.IsActive = userProject.IsActive;
                managerWorkTimeDto.CanUpdate = managerWorkTimeDto.Employee.Id == userId &&
                                               (timeService.Now - managerWorkTimeDto.Date).TotalDays < configurationManager.WorkTimeUpdateIntervalInDays;
            }

            return result;
        }

        public void UpdateWorkTime(int userId, UpdateWorkTimeDto workTime)
        {
            if ((timeService.Now - workTime.Date).TotalDays > configurationManager.WorkTimeUpdateIntervalInDays)
            {
                throw new InvalidOperationException();
            }
            var dummy = new WorkTime();
            var inDb = UoW.WorkTimeLog.GetById(workTime.Id, nameof(dummy.Issue) + "." + nameof(dummy.Issue.Project) + "." + nameof(dummy.Issue.Project.AssociatedMembers));
            if (inDb == null)
            {
                throw new NotFoundException("Worktime not found!");
            }

            if (inDb.Issue.Project.AssociatedMembers.Any(x => x.UserId == userId && !x.IsActive))
            {
                throw new UnauthorizedException("You are inactive in this project!");
            }

            if (inDb.EmployeeId != userId)
            {
                throw new UnauthorizedException("Not your worktime!");
            }

            if (inDb.Issue.IsClosed || inDb.Issue.Project.IsClosed)
            {
                throw new UnauthorizedException("Issue or project is closed!");
            }

            inDb.Name = workTime.Name;
            inDb.Description = workTime.Description;
            inDb.Date = workTime.Date;
            inDb.Hours = workTime.Hours;
            inDb.RowVersion = workTime.RowVersion;
            UoW.WorkTimeLog.Update(inDb);
            try
            {
                UoW.SaveChanges();
            }
            catch (ConcurrencyException)
            {
                throw new ConflictedException("Somebody already updated this worktime! Please do it again!");
            }
        }
    }
}
