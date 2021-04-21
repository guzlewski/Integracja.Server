using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace Integracja.Server.Infrastructure.Utilities
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings settings;

        public SmtpEmailSender(IOptions<SmtpSettings> emailSettings)
        {
            settings = emailSettings.Value;
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
