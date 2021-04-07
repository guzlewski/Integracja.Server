using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Integracja.Server.Web.Installers
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
