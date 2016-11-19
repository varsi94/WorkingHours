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

        public IssuesController(IWorkTimeManager worktimeManager)
        {
            this.worktimeManager = worktimeManager;
        }

        protected override void SetupManagers()
        {
            Managers.Add(worktimeManager);
        }

        [HttpGet]
        [Route("Issues/Details/{projectId}/{issueId}")]
        public async Task<ActionResult> Details(int projectId, int issueId, int? pageIndex = null)
        {
            try
            {
                var result = await worktimeManager.GetMyWorkTimesAsync(issueId, PageSize, pageIndex ?? 1);
                return View(new IssueDetailsModel {WorkTimes = result, ProjectId = projectId});
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