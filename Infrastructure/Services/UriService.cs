using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;

namespace Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UriService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetAbsoluteUrl()
        {
            return _httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();
        }

        public string GetAbsolutePath()
        {
            return _httpContextAccessor.HttpContext.Request.Path;
        }

        public Uri GetAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };
            return uriBuilder.Uri;

        }
    }
}
