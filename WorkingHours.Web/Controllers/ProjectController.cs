using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Shared.Dto;
using WorkingHours.Web.Extensions;

namespace WorkingHours.Web.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProjectManager projectManager;

        public ProjectController(IProjectManager projectManager)
        {
            this.projectManager = projectManager;
        }

        [HttpPost]
        [Route("api/projects/create")]
        [AuthorizeRoles(Roles.Manager)]
        public IHttpActionResult Create([FromBody] ProjectHeader projectHeader)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            var project = new Project
            {
                Name = projectHeader.Name,
                Deadline = projectHeader.Deadline
            };
            var id = projectManager.Add(project, User.Identity.GetUserId());
            return Created<object>(Url.Route(nameof(GetProjectInfo), new {id = id}), null);
        }

        [HttpGet]
        [Route("api/projects")]
        [Authorize]
        public IHttpActionResult ListProjects()
        {
            return Ok(projectManager.List(User.Identity.GetUserId()));
        }

        [HttpGet]
        [Route("api/project/{id}", Name = nameof(GetProjectInfo))]
        [Authorize]
        public IHttpActionResult GetProjectInfo(int id)
        {
            return Ok(projectManager.GetProjectInfo(id, User.Identity.GetUserId()));
        }

        [HttpPost]
        [Route("api/project/{projectId}/membersAdd")]
        [AuthorizeRoles(Roles.Manager)]
        public IHttpActionResult AddMembers(int projectId, Dictionary<int, Roles> membersToAdd)
        {
            projectManager.AddMembersToProject(projectId, User.Identity.GetUserId(), membersToAdd);
            return Ok();
        }

        [HttpPost]
        [Route("api/project/{projectId}/membersRemove")]
        [AuthorizeRoles(Roles.Manager)]
        public IHttpActionResult RemoveMembers(int projectId, List<int> membersToRemove)
        {
            projectManager.RemoveMembersFromProject(projectId, User.Identity.GetUserId(), membersToRemove);
            return Ok();
        }

        [HttpGet]
        [Route("api/project/report/{projectId}")]
        [Authorize]
        public IHttpActionResult GetReport(int projectId, [FromUri] DateTime? startDate = null, [FromUri] DateTime? endDate = null)
        {
            var message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content =
                new ByteArrayContent(projectManager.GetReport(User.Identity.GetUserId(), projectId, startDate, endDate));
            message.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping("valami.docx"));
            return ResponseMessage(message);
        }
    }
}
