using Application._Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserName()
        {
            var userName = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userName;
        }

        public string GetCurrentEmail()
        {
            var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return email;
        }
    }
}
