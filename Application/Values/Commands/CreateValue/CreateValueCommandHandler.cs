using Application.Common.CustomExceptions;
using Application.Common.HelperExtensions.Number;
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
            var entity = new Value(request.ValueNumber);

            _context.Values.Add(entity);
     
            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!success) 
                throw new Exception("Problem saving changes");

            //Publish
            //wait for all to finish
            //await _mediator.Publish(new NotificationMessage("teste@teste.com", "teste1@teste.com", "Body here","Subject"));

            //Fire and forget (Only use it if you 101% you sure i what you want)
            //var _ = Task.Run(() => _mediator.Publish(new NotificationMessage("teste@teste.com", "teste1@teste.com", "Body here", "Subject")));
            
            return entity.ValueId;
        }
    }
}
