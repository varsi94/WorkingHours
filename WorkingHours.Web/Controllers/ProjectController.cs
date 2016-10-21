using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkingHours.Bll.Dto;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Web.Extensions;
using WorkingHours.Web.Models;

namespace WorkingHours.Web.Controllers
{
    public class ProjectController : ApiController
    {
        private IProjectManager ProjectManager { get; }

        public ProjectController(IProjectManager projectManager)
        {
            ProjectManager = projectManager;
        }

        [HttpPost]
        [Route("api/projects/create")]
        [AuthorizeRoles(Roles.Manager)]
        public IHttpActionResult Create([FromBody] ProjectHeader projectHeader)
        {
            var project = new Project
            {
                Name = projectHeader.Name,
                Deadline = projectHeader.Deadline
            };
            ProjectManager.Add(project, User.Identity.GetUserId());
            return Ok();
        }

        [HttpGet]
        [Route("api/projects")]
        public IHttpActionResult ListProjects()
        {
            return Ok(ProjectManager.List(User.Identity.GetUserId()));
        }
    }
}
