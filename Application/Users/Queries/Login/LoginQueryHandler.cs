using Application.Common.CustomExceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new RestException(HttpStatusCode.Unauthorized);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

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
