using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Integracja.Server.Web.Areas.Identity.IdentityHostingStartup))]
namespace Integracja.Server.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}