using Application.Common.NotificationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISmsNotificationMessage
    {
        Task SendAsync(NotificationMessage message);
    }
}
