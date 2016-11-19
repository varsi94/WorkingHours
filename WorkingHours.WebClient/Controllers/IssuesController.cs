using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkingHours.Client.Exceptions;
using WorkingHours.Client.Interfaces;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;
using WorkingHours.WebClient.Common;
using WorkingHours.WebClient.Extensions;
using WorkingHours.WebClient.Models;

namespace WorkingHours.WebClient.Controllers
{
    [Authorize]
    public class IssuesController : BaseController
    {
        private const int PageSize = 20;
        private readonly IWorkTimeManager worktimeManager;
        private readonly IProjectManager projectManager;

        public IssuesController(IWorkTimeManager worktimeManager, IProjectManager projectManager)
        {
            this.worktimeManager = worktimeManager;
            this.projectManager = projectManager;
        }

        protected override void SetupManagers()
        {
            Managers.Add(worktimeManager);
            Managers.Add(projectManager);
        }

        [HttpGet]
        [Route("Issues/Details/{projectId}/{issueId}")]
        public async Task<ActionResult> Details(int projectId, int issueId, int? pageIndex = null)
        {
            try
            {
                var project = await projectManager.GetProjectAsync(projectId);
                var issue = project.Issues.SingleOrDefault(x => x.Id == issueId);
                if (issue == null)
                {
                    return HttpNotFound();
                }

                var result = await worktimeManager.GetMyWorkTimesAsync(issueId, PageSize, pageIndex ?? 1);
                return
                    View(new IssueDetailsModel<WorkTimeDto>
                    {
                        WorkTimes = result,
                        ProjectId = projectId,
                        IssueName = issue.Name,
                        IssueId = issueId,
                        IsManager = project.Members.Any(x => x.Id == User.GetUserId() && x.RoleInProjectEnum == Roles.Manager)
                    });
            }
            catch (UnauthorizedAccessException)
            {
                return HttpNotFound();
            }
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        [Route("Issues/ManagerDetails/{projectId}/{issueId}")]
        public async Task<ActionResult> DetailsForManagers(int projectId, int issueId, int? pageIndex = null)
        {
            try
            {
                var project = await projectManager.GetProjectAsync(projectId);
                if (!project.Members.Any(x => x.Id == User.GetUserId() && x.RoleInProjectEnum == Roles.Manager))
                {
                    return HttpNotFound();
                }

                var issue = project.Issues.SingleOrDefault(x => x.Id == issueId);
                if (issue == null)
                {
                    return HttpNotFound();
                }

                var result = await worktimeManager.GetWorkTimesForManagerAsync(issueId, PageSize, pageIndex ?? 1);
                return
                    View(new IssueDetailsModel<ManagerWorkTimeDto>
                    {
                        WorkTimes = result,
                        ProjectId = projectId,
                        IssueName = issue.Name,
                        IssueId = issueId
                    });
            }
            catch (UnauthorizedAccessException)
            {
                return HttpNotFound();
            }
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
        }
    }
}