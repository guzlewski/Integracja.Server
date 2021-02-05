using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Web.Services
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }

    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings settings;

        public SmtpEmailSender(IOptionsMonitor<SmtpSettings> emailSettings)
        {
            settings = emailSettings.CurrentValue;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(settings.Host, settings.Port)
            {
                Credentials = new NetworkCredential(settings.UserName, settings.Password),
                EnableSsl = settings.EnableSSL
            };

            return client.SendMailAsync(new MailMessage(settings.From, email, subject, message) { IsBodyHtml = true });
        }
    }
}
