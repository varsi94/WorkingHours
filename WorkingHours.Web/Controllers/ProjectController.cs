using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Shared.Dto;
using WorkingHours.Web.Extensions;

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
            var id = ProjectManager.Add(project, User.Identity.GetUserId());
            return Created<object>(Url.Route(nameof(GetProjectInfo), new {id = id}), null);
        }

        [HttpGet]
        [Route("api/projects")]
        [Authorize]
        public IHttpActionResult ListProjects()
        {
            return Ok(ProjectManager.List(User.Identity.GetUserId()));
        }

        [HttpGet]
        [Route("api/project/{id}", Name = nameof(GetProjectInfo))]
        [Authorize]
        public IHttpActionResult GetProjectInfo(int id)
        {
            return Ok(ProjectManager.GetProjectInfo(id, User.Identity.GetUserId()));
        }
    }
}
