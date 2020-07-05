using Application.Common.NotificationServices;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(NotificationMessage message);
    }
}
