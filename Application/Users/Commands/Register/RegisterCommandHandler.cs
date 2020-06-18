
using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtGenerator _jwt;

        public RegisterCommandHandler(IIdentityService identityService, IJwtGenerator jwt)
        {
            _identityService = identityService;
            _jwt = jwt;
        }
        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var user = new User
            {
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                Email = request.Email,
            };

            var (result,_) = await _identityService.CreateUserAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = request.DisplayName,
                    UserName = request.UserName,
                    Token = _jwt.CreateToken(user)
                };
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RegisterErrors = result.Errors});
                //throw new Exception("Problem creating user");
            }
        }
    }
}
