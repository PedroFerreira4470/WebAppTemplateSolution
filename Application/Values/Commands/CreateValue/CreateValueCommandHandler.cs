using Domain.Entities;
using MediatR;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Values.Commands.CreateValue
{
    public class CreateValueCommandHandler : IRequestHandler<CreateValueCommand, int>
    {
        private readonly TemplateDbContext _context;

        public CreateValueCommandHandler(TemplateDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateValueCommand request, CancellationToken cancellationToken)
        {
            var entity = new Value
            {
                ValueNumber = request.ValueNumber
            };

            _context.Values.Add(entity);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return entity.ValueId;
        
            throw new Exception("Problem saving changes");

        }
    }
}
