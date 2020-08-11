using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.SignalR
{
    public class CommentHub : Hub
    {
        private readonly IMediator _mediator;

        public CommentHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendCommentAsync(/*mediator command (e.g Create message), */CancellationToken cancellationToken)
        {
            //claim may be wrong var username = Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            _ = _mediator;
            //command.UserName = username;

            //var comment = await mediator.Send(command);

            await Clients.All.SendAsync("ReceiveComment", new {/*command eg(messageDTO)*/Example = "" }, cancellationToken);
        }
    }
}
