using Application.Common.Interfaces;
using Application.Common.NotificationServices;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public async Task SendAsync(NotificationMessage notificationMessage)
        {
            //Teste Purpose (not working, try using smtp4dev)
            var smtpClient = new SmtpClient("localhost");
            var mailMessage = new MailMessage
            {
                From = new MailAddress(notificationMessage.From),
                Subject = notificationMessage.Subject,
                Body = notificationMessage.Body,
            };

            mailMessage.To.Add(new MailAddress(notificationMessage.To));

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Sending email to {notificationMessage.To} From {notificationMessage.From} with subject {notificationMessage.Subject}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error Sending email to {notificationMessage.To} From {notificationMessage.From} with subject {notificationMessage.Subject}");
            }

        }
    }
}
