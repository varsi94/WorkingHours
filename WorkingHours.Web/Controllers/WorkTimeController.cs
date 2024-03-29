﻿using System;
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

        [HttpGet]
        [Route("api/worktimes/{issueId}")]
        [Authorize]
        public IHttpActionResult GetMyWorkTimes(int issueId, [FromUri] int? pageSize = null,
            [FromUri] int? pageIndex = null)
        {
            var pagingInfo = new PagingInfo
            {
                PageIndex = pageIndex ?? 1,
                PageSize = pageSize ?? 10
            };

            return Ok(workTimeManager.GetMyWorkTimes(User.Identity.GetUserId(), issueId, pagingInfo));
        }

        [HttpGet]
        [Route("api/worktimes/manager/{issueId}")]
        [AuthorizeRoles(Roles.Manager)]
        public IHttpActionResult GetManagerWorkTimes(int issueId, [FromUri] int? pageSize = null,
            [FromUri] int? pageIndex = null)
        {
            var pagingInfo = new PagingInfo
            {
                PageIndex = pageIndex ?? 1,
                PageSize = pageSize ?? 10
            };

            return Ok(workTimeManager.GetWorkTimesForManager(User.Identity.GetUserId(), issueId, pagingInfo));
        }

        [HttpPut]
        [Route("api/worktime/")]
        [Authorize]
        public IHttpActionResult UpdateWorkTime([FromBody] UpdateWorkTimeDto workTime)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                workTimeManager.UpdateWorkTime(User.Identity.GetUserId(), workTime);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "You can update a work item in a week only!"));
            }
        }

        [HttpDelete]
        [Route("api/worktime/{workTimeId}")]
        [Authorize]
        public IHttpActionResult DeleteWorkTime(int workTimeId)
        {
            try
            {
                workTimeManager.DeleteWorkTime(User.Identity.GetUserId(), workTimeId);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return
                    ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "You can update a work item in a week only!"));
            }
        }
    }
}
