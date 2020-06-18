using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
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

        public string GetUserId()
        {
            var userId = _httpContextAccessor
                .HttpContext
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
        public string GetUsername()
        {
            var userName = _httpContextAccessor
                .HttpContext
                .User?
                .FindFirstValue(ClaimTypes.Name);
            return userName;
        }

        public string GetEmail()
        {
            var email = _httpContextAccessor
                .HttpContext
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?
                .Value;
            return email;
        }

        public string GetUserGlobalization() {
            //TODO (should return globalization (e.g {pt-PT}) from header)
            //https://dotnetcoretutorials.com/2017/06/22/request-culture-asp-net-core/
            return _httpContextAccessor
                .HttpContext
                .Request
                .GetTypedHeaders()
                .AcceptLanguage
                .ToString();
        }
    }
}
