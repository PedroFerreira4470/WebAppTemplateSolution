using MediatR;

namespace Application.Common.NotificationServices
{
    public class NotificationMessage : INotification
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        private NotificationMessage() { }
        public NotificationMessage(string from, string to, string body)
        {
            From = from;
            To = to;
            Body = body;
        }
        public NotificationMessage(string from, string to, string body, string subject)
        {
            From = from;
            To = to;
            Body = body;
            Subject = subject;
        }
    }
}
