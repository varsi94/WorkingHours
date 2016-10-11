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
                    context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else if (ex is UnauthorizedException)
                {
                    context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
                else if (ex is InternalServerException)
                {
                    context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}