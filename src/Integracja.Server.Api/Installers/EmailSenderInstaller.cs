using Integracja.Server.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Api.Installers
{
    public class EmailSenderInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailSender, SmtpEmailSender>();
        }
    }
}
