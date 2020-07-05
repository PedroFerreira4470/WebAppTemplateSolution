using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.NotificationServices
{
    public class EmailNotificationHandler : INotificationHandler<NotificationMessage>
    {
        private readonly IEmailSender _emailSender;

        public EmailNotificationHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public Task Handle(NotificationMessage message, CancellationToken cancellationToken)
        {
            return _emailSender.SendAsync(message);
        }
    }
}
