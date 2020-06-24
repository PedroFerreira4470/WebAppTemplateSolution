using Application.Common.NotificationServices;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISmsNotificationMessage
    {
        Task SendAsync(NotificationMessage message);
    }
}
