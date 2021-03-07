using System.Text;
using Integracja.Server.Infrastructure.Settings;
using Integracja.Server.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Integracja.Server.Api.Installers
{
    public class JwtInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingKey,
                ValidIssuer = configuration["Jwt:ValidIssuer"],
                ValidAudience = configuration["Jwt:ValidAudience"]
            };

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddScoped<ApplicationJwtBearerEvents>();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(c =>
            {
                c.EventsType = typeof(ApplicationJwtBearerEvents);
                c.SaveToken = false;
                c.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
