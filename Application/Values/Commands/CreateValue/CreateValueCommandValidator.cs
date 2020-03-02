using FluentValidation;

namespace Application.Values.Commands.CreateValue
{
    public class CreateValueCommandValidator : AbstractValidator<CreateValueCommand>
    {
        public CreateValueCommandValidator()
        {
            RuleFor(v => v.ValueNumber)
                .NotEmpty().WithMessage("Value Number can't be empty!")
                .LessThanOrEqualTo(int.MaxValue).WithMessage("This value is not supported!");
        }
    }
}
