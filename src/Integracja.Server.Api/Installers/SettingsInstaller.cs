using Integracja.Server.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Api.Installers
{
    public class SettingsInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.Configure<PictureSettings>(configuration.GetSection("Picture"));
        }
    }
}
