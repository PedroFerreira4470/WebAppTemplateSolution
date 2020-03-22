using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Values.Commands.CreateValue
{
    public class CreateValueCommandHandler : IRequestHandler<CreateValueCommand, int>
    {
        private readonly ITemplateDbContext _context;

        public CreateValueCommandHandler(ITemplateDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateValueCommand request, CancellationToken cancellationToken)
        {
            var entity = new Value { ValueNumber = request.ValueNumber };

            entity.HandleBLL(example: "teste"); //Business Logic Layer

            _context.Values.Add(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return entity.ValueId;

            throw new Exception("Problem saving changes");

        }
    }
}
