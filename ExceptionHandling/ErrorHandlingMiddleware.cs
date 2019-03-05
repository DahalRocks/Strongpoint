using LoggerContract;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerManager _logger;

        public ErrorHandlingMiddleware(RequestDelegate next,ILoggerManager logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogDebug(exception.Message);
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is ArgumentNullException) code = HttpStatusCode.NotFound;
            else if (exception is ArithmeticException) code = HttpStatusCode.NotAcceptable;
            else code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = exception.Message, errorCode=code });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
