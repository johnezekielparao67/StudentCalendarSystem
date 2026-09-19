using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace StudentCalendar.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(
            string eventName,
            string eventDate,
            string eventTime,
            string eventDescription,
            string recipientEmail)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _configuration["EmailSettings:FromName"],
                _configuration["EmailSettings:FromEmail"]
            ));

            message.To.Add(new MailboxAddress(
                "Student",
                recipientEmail
            ));

            message.Subject = "Student Calendar Notification";

            message.Body = new TextPart("plain")
            {
                Text = $"Hello!\n\n" +
           $"A new event has been added to your Student Calendar.\n\n" +
           $"Event: {eventName}\n" +
           $"Date: {eventDate}\n" +
           $"Time: {eventTime}\n" +
           $"Description: {eventDescription}\n\n" +
           $"Thank you,\n" +
           $"Student Calendar"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(
                    _configuration["EmailSettings:SmtpHost"],
                    int.Parse(_configuration["EmailSettings:SmtpPort"]),
                    SecureSocketOptions.StartTls
                );

                client.Authenticate(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]
                );

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
