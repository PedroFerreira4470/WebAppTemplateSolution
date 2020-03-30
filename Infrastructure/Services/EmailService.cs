using Application.Common.Interfaces;
using Application.Common.NotificationServices;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailNotificationMessage
    {
        public Task SendAsync(NotificationMessage message)
        {
            //TODO SEND Email (smt)
            //var mailMessage = new MailMessage(message.From, message.To)
            //{
            //    Subject = message.Subject,
            //    Body = message.Body
            //};

            //var smtp = new SmtpClient();
            //await smtp.SendMailAsync(mailMessage);
            return Task.CompletedTask;
        }
    }
}
