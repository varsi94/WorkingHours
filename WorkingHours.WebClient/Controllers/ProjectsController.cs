using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkingHours.Client.Interfaces;
using WorkingHours.WebClient.Common;

namespace WorkingHours.WebClient.Controllers
{
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

        [Authorize]
        public async Task<ActionResult> MyProjects()
        {
            var result = await projectManager.GetMyProjectsAsync();
            return View(result);
        }
    }
}