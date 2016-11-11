using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using WorkingHours.Bll.Exceptions;

namespace WorkingHours.Web.Extensions
{
    public class BllExceptionFilterAttribute : ExceptionFilterAttribute
    {   
        public override void OnException(HttpActionExecutedContext context)
        {
            var ex = context.Exception as BllException;
            if (ex != null)
            {
                if (ex is NotFoundException)
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                }
                else if (ex is UnauthorizedException)
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                }
                else if (ex is InternalServerException)
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                }
                else if (ex is ConflictedException)
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
                }
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}