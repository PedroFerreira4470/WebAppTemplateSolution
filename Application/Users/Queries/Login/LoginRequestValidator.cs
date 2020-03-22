using FluentValidation;

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
