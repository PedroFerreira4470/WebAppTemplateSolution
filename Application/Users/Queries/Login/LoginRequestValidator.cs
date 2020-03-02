using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Queries.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginQuery>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
