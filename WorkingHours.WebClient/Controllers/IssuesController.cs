using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkingHours.Client.Exceptions;
using WorkingHours.Client.Interfaces;
using WorkingHours.WebClient.Common;
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
                return View(new IssueDetailsModel {WorkTimes = result, ProjectId = projectId, IssueName = issue.Name });
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