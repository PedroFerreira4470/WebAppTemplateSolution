using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Commands.Register
{
    public class RegisterCommand : IRequest<UserDto>
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
