using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Web.Installers
{
    public class ServicesInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IStorageService, BlobStorageService>();

            services.AddScoped<IPictureService, PictureService>();
        }
    }
}
