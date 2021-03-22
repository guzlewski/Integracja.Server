using System;
using System.IO;
using Integracja.Server.Api.Utilities;
using Integracja.Server.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Integracja.Server.Api.Installers
{
    public class SwaggerInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();

                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"));

                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(ApplicationDbContext).Assembly.GetName().Name}.xml"));

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Integracja.Server.Api",
                    Version = "v1"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                swagger.OperationFilter<AuthorizeOperationFilter>();

                swagger.OperationFilter<MobileOperationFilter>();
            });
        }
    }
}
