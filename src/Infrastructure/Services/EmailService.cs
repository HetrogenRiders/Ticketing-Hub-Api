using System.Net;
using System.Net.Mail;
using TicketingHub.Api.Common.Interfaces;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var smtpHost = _configuration["MailSettings:Host"];
        var smtpPort = int.Parse(_configuration["MailSettings:Port"]);
        var smtpUser = _configuration["MailSettings:UserName"];
        var smtpPass = _configuration["MailSettings:Password"];
        var fromEmail = _configuration["MailSettings:FromAddress"];

        if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass))
        {
            throw new Exception("Email configuration is missing or invalid.");
        }

        using (var client = new SmtpClient(smtpHost, smtpPort))
        {
            client.Credentials = new NetworkCredential(smtpUser, smtpPass);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
