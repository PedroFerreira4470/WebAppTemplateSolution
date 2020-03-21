
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
        private readonly ITemplateDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwt;

        public RegisterCommandHandler(ITemplateDbContext context, UserManager<User> userManager, IJwtGenerator jwt)
        {
            _context = context;
            _userManager = userManager;
            _jwt = jwt;
        }
        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (_context.Users.Where(p => p.Email == request.Email).Any())
                throw new RestException(HttpStatusCode.BadRequest, "Email Already Exist");
            if (_context.Users.Where(p => p.UserName == request.UserName).Any())
                throw new RestException(HttpStatusCode.BadRequest, "Username Already Exist");

            var user = new User
            {
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = request.DisplayName,
                    UserName = request.UserName,
                    Token = _jwt.CreateToken(user)
                };
            }
            throw new Exception("Problem creating user");
        }
    }
}
