using Application.Common.Contracts.V1.ResponseType;
using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using Application.Common.MethodExtensions;
using Domain.Entities;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<RegisterCommandResponseDto>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtGenerator _jwt;

        public RegisterCommandHandler(IIdentityService identityService, IJwtGenerator jwt)
        {
            _identityService = identityService;
            _jwt = jwt;
        }
        public async Task<Response<RegisterCommandResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var user = new User
            {
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                Email = request.Email,
            };

            var (result, _) = await _identityService.CreateUserAsync(user, request.Password);

            if (result.Succeeded.IsNotTrue())
            {
                throw new RestException(HttpStatusCode.BadRequest, "RegisterErrors", result.Errors);
            }

            return new Response<RegisterCommandResponseDto>(new RegisterCommandResponseDto
            {
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                Token = await _jwt.CreateTokenAsync(user)
            });


        }
    }
}
