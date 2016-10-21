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
    public class IssueController : ApiController
    {
        private IIssueManager IssueManager { get; }

        public IssueController(IIssueManager issueManager)
        {
            IssueManager = issueManager;
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Manager)]
        [Route("api/issues/create/{projectId}")]
        public IHttpActionResult CreateIssueForProject(int projectId, IssueHeader issueHeader)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            var issue = new Issue
            {
                Deadline = issueHeader.Deadline,
                Name = issueHeader.Name,
                IsClosed = false,
                Description = issueHeader.Description
            };
            IssueManager.AddIssueToProject(projectId, issue, User.Identity.GetUserId());
            return Ok();
        }
    }
}
