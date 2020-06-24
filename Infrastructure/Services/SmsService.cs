using Application.Common.Interfaces;
using Application.Common.NotificationServices;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SmsService : ISmsNotificationMessage
    {
        public Task SendAsync(NotificationMessage message)
        {
            //TODO SEND Sms
            return Task.CompletedTask;
        }
    }
}
