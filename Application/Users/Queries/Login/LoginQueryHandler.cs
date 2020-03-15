using Application._CustomExceptions;
using Application._Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtGenerator jwtGenerator;

        public LoginQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtGenerator = jwtGenerator;
        }
        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized);
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                //generate Token
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token = jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };
            }
            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}
