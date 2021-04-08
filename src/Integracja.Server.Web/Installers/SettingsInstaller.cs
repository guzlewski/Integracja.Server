using Integracja.Server.Infrastructure.Settings;
using Integracja.Server.Web.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Web.Installers
{
    public class SettingsInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));

            services.Configure<PictureSettings>(configuration.GetSection("Picture"));

            services.Configure<DefaultSettings>(configuration.GetSection("Default"));
        }
    }
}
