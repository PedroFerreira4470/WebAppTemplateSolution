using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IIdentityService _identityService;

        public LoginQueryHandler(IIdentityService identityService, IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
            _identityService = identityService;
        }
        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var (user, result) = await _identityService.SignInAsync(request.Email, request.Password);

            if (result.Succeeded)
            {
                //generate Token
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };
            }
            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}
