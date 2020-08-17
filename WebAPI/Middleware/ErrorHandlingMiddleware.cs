using Application.Common.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }

        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            object errors = null;

            switch (ex)
            {
                case RestException re:
                    var error = JsonSerializer.Serialize(re.Errors);
                    logger.LogError(re, "Error: {@Status} {@Error}", re.Code, error);
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case { } e:
                    logger.LogError(e, "Uncaught Internal Error");
                    errors = new Dictionary<string, string[]>
                    {
                        {"Internal Error",  new [] {String.IsNullOrWhiteSpace(e.Message) ? "Something went wrong!" : e.Message } }
                    };

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
