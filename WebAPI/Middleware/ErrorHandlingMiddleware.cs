using Application.Common.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object errors = null;

            switch (ex)
            {
                case RestException re:
                    Log.Error($"REST ERROR: { (int)re.Code } {re.Errors}",re);
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    Log.Error($"SERVER ERROR: { (int)HttpStatusCode.InternalServerError } {e.Message}",e);
                    errors = String.IsNullOrWhiteSpace(e.Message) ? "Something went wrong" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            if (errors != null)
            {
                var result = JsonSerializer.Serialize(new { errors });

                await context.Response.WriteAsync(result);
            }
        }
    
    }
}
