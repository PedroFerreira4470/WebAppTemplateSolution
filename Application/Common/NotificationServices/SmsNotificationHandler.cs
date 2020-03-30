using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.NotificationServices
{
    public class SmsNotificationHandler : INotificationHandler<NotificationMessage>
    {
        private readonly ISmsNotificationMessage _notificationMessage;

        public SmsNotificationHandler(ISmsNotificationMessage notificationMessage)
        {
            _notificationMessage = notificationMessage;
        }
        public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
        {
            return _notificationMessage.SendAsync(notification);
        }
    }
}
