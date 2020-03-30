using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.NotificationServices
{
    public class EmailNotificationHandler : INotificationHandler<NotificationMessage>
    {
        private readonly IEmailNotificationMessage _notificationMessage;

        public EmailNotificationHandler(IEmailNotificationMessage notificationMessage)
        {
            _notificationMessage = notificationMessage;
        }
        public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
        {
            return _notificationMessage.SendAsync(notification);
        }
    }
}
