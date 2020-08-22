using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.V1.Values.Commands.CreateValue
{
    public class CreateValueCommandValidator : AbstractValidator<CreateValueCommand>
    {

        private readonly ITemplateDbContext _context;
        public CreateValueCommandValidator(ITemplateDbContext context)
        {
            _context = context;

            RuleFor(v => v.ValueNumber)
                .NotNull().WithMessage("Value can't be empty!")
                .GreaterThanOrEqualTo(0).WithMessage("Value can't be negative!")
                .MustAsync(BeUniqueValueAsync).WithMessage("The specified value already exists.");
        }

        public async Task<bool> BeUniqueValueAsync(int valueNumber, CancellationToken cancellationToken)
            => await _context.Values.AnyAsync(v => v.ValueNumber == valueNumber, cancellationToken) == false;
    }
}
