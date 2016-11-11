using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Shared.Dto;
using WorkingHours.Web.Extensions;

namespace WorkingHours.Web.Controllers
{
    public class WorkTimeController : ApiController
    {
        private readonly IWorkTimeManager workTimeManager;

        public WorkTimeController(IWorkTimeManager workTimeManager)
        {
            this.workTimeManager = workTimeManager;
        }

        [HttpPost]
        [Route("api/worktime/create/{issueId}")]
        [Authorize]
        public IHttpActionResult CreateWorkItem(int issueId, [FromBody] WorkTimeDto workTimeDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            workTimeManager.AddWorkItem(issueId, User.Identity.GetUserId(), workTimeDto);
            return Ok();
        }
    }
}
