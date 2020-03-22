using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.SignalR
{
    public class CommentHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IMediator _mediator;

        public CommentHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(/*mediator command (e.g Create message), */CancellationToken cancellationToken)
        {
            var username = Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            _ = _mediator;
            //command.UserName = username;

            //var comment = await mediator.Send(command);

            await Clients.All.SendAsync("ReceiveComment",/*command eg(messageDTO)*/"",cancellationToken);
        }
    }
}
