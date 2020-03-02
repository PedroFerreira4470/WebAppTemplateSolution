using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Commands.Register
{
   public class RegisterRequestValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).PasswordValidator();
        }

      }

     public static class PasswordValidatorExtension
    {
        public static IRuleBuilder<T,string> PasswordValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
        {

            var options = ruleBuilder.NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least 1 number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");

            return options;
        }
    }

}
