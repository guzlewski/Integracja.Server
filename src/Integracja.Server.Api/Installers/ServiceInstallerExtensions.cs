using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracja.Server.Api.Installers
{
    public static class ServiceInstallerExtensions
    {
        public static void InstallServices(this IServiceCollection services, Assembly assembly, IConfiguration configuration)
        {
            var installers = assembly.GetExportedTypes()
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(IServiceInstaller).IsAssignableFrom(c))
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>()
                .ToList();

            installers.ForEach(i => i.InstallServices(services, configuration));
        }
    }
}
