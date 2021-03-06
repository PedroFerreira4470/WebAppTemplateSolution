﻿using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly IMediator _mediator;

        public NotificationHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendNotificationAsync(/*mediator command (e.g Create notification), */CancellationToken cancellationToken)
        {
            //claim may be wrong var username = Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            _ = _mediator;
            //command.UserName = username;

            //var comment = await mediator.Send(command);

            await Clients.All.SendAsync("ReceiveNotification", new {/*command eg(notificationDTO)*/example = "" }, cancellationToken);
        }
    }
}
