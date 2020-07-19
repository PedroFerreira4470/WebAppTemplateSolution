using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var httpcontext = httpContextAccessor.HttpContext;
            UserId = httpcontext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserName = httpcontext?.User?.FindFirstValue(ClaimTypes.Name);
            Email = httpcontext?.User?.FindFirstValue(ClaimTypes.Email);
            var t = httpcontext?.Request?.GetTypedHeaders();
            //TODO (should return globalization (e.g {pt-PT}) from header)
            //https://dotnetcoretutorials.com/2017/06/22/request-culture-asp-net-core/
            UserLanguage = httpcontext?.Request?.GetTypedHeaders().AcceptLanguage.FirstOrDefault().ToString();
        }

        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string UserLanguage { get; private set; }

    }
}
