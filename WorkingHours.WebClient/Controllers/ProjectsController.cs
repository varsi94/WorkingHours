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
    public class ProjectsController : BaseController
    {
        private readonly IProjectManager projectManager;

        public ProjectsController(IProjectManager projectManager)
        {
            this.projectManager = projectManager;
        }

        protected override void SetupManagers()
        {
            Managers.Add(projectManager);
        }

        [HttpGet]
        public async Task<ActionResult> MyProjects()
        {
            var result = await projectManager.GetMyProjectsAsync();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Report(int id, ReportIntervalModel interval)
        {
            try
            {
                var project = await projectManager.GetProjectAsync(id);
                var result = await projectManager.GetReportAsync(id, interval.StartDate, interval.EndDate);
                return File(result, MimeMapping.GetMimeMapping(".docx"), project.Name + ".docx");
            }
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Issues(int id)
        {
            try
            {
                var project = await projectManager.GetProjectAsync(id);
                return View(new ProjectDetailsModel { ProjectId = id, Issues = project.Issues, ProjectName = project.Name });
            }
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        [Route("Projects/Members/{projectId}")]
        public async Task<ActionResult> Members(int projectId)
        {
            try
            {
                var project = await projectManager.GetProjectAsync(projectId);
                return View(new MembersModel {Members = project.Members, ProjectName = project.Name});
            }
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return HttpNotFound();
            }
        }
    }
}