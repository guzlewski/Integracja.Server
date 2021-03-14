using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Api.Installers
{
    public class ControllersInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
               .AddJsonOptions(opts =>
               {
                   opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
               })
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.SuppressMapClientErrors = true;
               });
        }
    }
}
