using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using TicketingHub.Api.Common.Interfaces;

namespace TicketingHub.Api.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _config;

        public EmailService(ILogger<EmailService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task SendEmailAsync(IEnumerable<string> recipients, string subject, string body)
        {
            var smtpHost = _config["Email:SmtpHost"];
            var smtpUser = _config["Email:Username"];
            var smtpPass = _config["Email:Password"];
            var smtpPort = int.Parse(_config["Email:Port"] ?? "587");
            var sender = _config["Email:From"];

            using var smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            foreach (var email in recipients.Where(e => !string.IsNullOrWhiteSpace(e)))
            {
                var mail = new MailMessage(sender, email, subject, body)
                {
                    IsBodyHtml = true
                };

                await smtpClient.SendMailAsync(mail);
                _logger.LogInformation("Email sent to {Email}.", email);
            }
        }
    }
}
