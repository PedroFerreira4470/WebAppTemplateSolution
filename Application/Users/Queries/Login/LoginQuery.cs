using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Queries.Login
{
    public class LoginQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
