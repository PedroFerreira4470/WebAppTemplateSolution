using Application.Common.Contracts.V1.ResponseType;
using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Users.Commands.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Response<LoginDto>>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IIdentityService _identityService;

        public LoginQueryHandler(IIdentityService identityService, IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
            _identityService = identityService;
        }
        public async Task<Response<LoginDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var (user, result) = await _identityService.SignInAsync(request.Email, request.Password);

            if (result.Succeeded)
            {
                //generate Token
                return new Response<LoginDto>(new LoginDto
                {
                    DisplayName = user.DisplayName,
                    Token = await _jwtGenerator.CreateTokenAsync(user),
                    UserName = user.UserName,
                });
            }

            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}
