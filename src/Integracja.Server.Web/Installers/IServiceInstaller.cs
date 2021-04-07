using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Web.Installers
{
    public interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
