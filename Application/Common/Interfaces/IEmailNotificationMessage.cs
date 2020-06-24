using Application.Common.NotificationServices;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailNotificationMessage
    {
        Task SendAsync(NotificationMessage message);
    }
}
