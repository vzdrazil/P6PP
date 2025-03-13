using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace NotificationService.API.Services
{
    public class MailAppService
    {
        public class SmtpSettings
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool EnableSsl { get; set; }
            public string From { get; set; }
        }

        private readonly IConfiguration _configuration;
        public MailAppService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(IList<string> addresates, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            foreach (var to in addresates)
            {
                mailMessage.To.Add(to);
            }

            if (!mailMessage.To.Any())
            {
                return;
            }

            using (var smtpClient = new SmtpClient(smtpSettings.Host, smtpSettings.Port))
            {
                smtpClient.EnableSsl = smtpSettings.EnableSsl;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        
    }
}
