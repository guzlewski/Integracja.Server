using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Integracja.Server.Api.Services
{
    public class AuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authorize = HasAttribute<AuthorizeAttribute>(context) && !HasAttribute<AllowAnonymousAttribute>(context);

            if (authorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            }
                        ] = Array.Empty<string>()
                    }
                };
            }
        }

        private static bool HasAttribute<T>(OperationFilterContext context)
        {
            return context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<T>().Any() ||
              context.MethodInfo.GetCustomAttributes(true).OfType<T>().Any();
        }
    }
}
