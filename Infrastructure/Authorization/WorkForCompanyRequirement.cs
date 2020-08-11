using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization
{
    public class WorkForCompanyRequirement : IAuthorizationRequirement
    {
        public string DomainMain { get; set; }

        public WorkForCompanyRequirement(string domainMain)
        {
            DomainMain = domainMain;
        }
    }

    public class WorkForCompanyRequirementHandler : AuthorizationHandler<WorkForCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorkForCompanyRequirement requirement)
        {
            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            if (userEmailAddress.EndsWith(requirement.DomainMain))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
