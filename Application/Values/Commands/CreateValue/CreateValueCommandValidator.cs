using FluentValidation;

namespace Application.Values.Commands.CreateValue
{
    public class CreateValueCommandValidator : AbstractValidator<CreateValueCommand>
    {
        public CreateValueCommandValidator()
        {
            RuleFor(v => v.ValueNumber)
                .NotNull().WithMessage("Value Number can't be empty!");
        }
    }
}
