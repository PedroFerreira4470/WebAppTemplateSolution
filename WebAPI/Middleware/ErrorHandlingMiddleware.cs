using Application.Common.CustomExceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
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

            context.Response.ContentType = "application/json";
            switch (ex)
            {
                case RestException re:
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case { } e:
                    _logger.LogError(e, "Uncaught Internal Error");
                    errors = new Dictionary<string, string[]>
                    {
                        {"Server Error",  new [] {
                            GetErrorMessage(e.Message, _env.IsDevelopment())
                        } }
                    };

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (errors is not null)
            {
                var result = JsonSerializer.Serialize(new { errors });
                await context.Response.WriteAsync(result, Encoding.UTF8);
            }
        }

        private string GetErrorMessage(string message, bool isDevelopment)
            => isDevelopment && string.IsNullOrWhiteSpace(message) == false ? message : "Something unexpectable went wrong!";
    }
}
