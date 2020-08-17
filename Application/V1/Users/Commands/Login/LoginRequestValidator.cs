using FluentValidation;

namespace Application.V1.Users.Commands.Login
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
