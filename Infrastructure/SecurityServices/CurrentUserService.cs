﻿using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.SecurityServices
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var httpcontext = httpContextAccessor.HttpContext;
            UserId = httpcontext?.User?.FindFirstValue("userId");
            UserName = httpcontext?.User?.FindFirstValue(ClaimTypes.Name);
            Email = httpcontext?.User?.FindFirstValue(ClaimTypes.Email);
            var t = httpcontext?.Request?.GetTypedHeaders();
            //TODO (should return globalization (e.g {pt-PT}) from header)
            //https://dotnetcoretutorials.com/2017/06/22/request-culture-asp-net-core/
            UserLanguage = httpcontext?.Request?.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.ToString();
        }

        public string UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public string UserLanguage { get; }

    }
}
