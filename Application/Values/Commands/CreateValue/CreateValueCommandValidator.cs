using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Values.Commands.CreateValue
{
    public class CreateValueCommandValidator : AbstractValidator<CreateValueCommand>
    {

        private readonly ITemplateDbContext _context;
        public CreateValueCommandValidator(ITemplateDbContext context)
        {
            _context = context;

            RuleFor(v => v.ValueNumber)
                .NotNull().WithMessage("Value can't be empty!")
                .MustAsync(BeUniqueValueAsync).WithMessage("The specified value already exists."); ;
        }

        public async Task<bool> BeUniqueValueAsync(int ValueNumber, CancellationToken cancellationToken)
        {
            return !await _context.Values.AnyAsync(v => v.ValueNumber == ValueNumber);
        }
    }
}
