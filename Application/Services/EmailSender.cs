using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CS330_Fall2024_FinalProject.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var password = Environment.GetEnvironmentVariable("ICLOUD_APP_PASSWORD") ?? emailSettings["AppPassword"];


            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError("ICLOUD_APP_PASSWORD is not set or is empty.");
                throw new InvalidOperationException("ICLOUD_APP_PASSWORD is not set.");
            }

            _logger.LogInformation("Using iCloud SMTP password: {Password}", password); //  don't forget to remove in production

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    _logger.LogInformation("Connecting to SMTP server...");
                    await client.ConnectAsync(emailSettings["SMTPServer"], int.Parse(emailSettings["Port"]), MailKit.Security.SecureSocketOptions.StartTls);

                    _logger.LogInformation("Authenticating with SMTP server...");
                    await client.AuthenticateAsync(emailSettings["SenderEmail"], password);

                    _logger.LogInformation("Sending email...");
                    await client.SendAsync(message);

                    _logger.LogInformation("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to send email: {ex.Message}");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }

    }
}
