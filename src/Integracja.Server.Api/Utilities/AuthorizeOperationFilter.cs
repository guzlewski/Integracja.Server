using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Integracja.Server.Api.Utilities
{
    public class AuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!HasAttribute<AuthorizeAttribute>(context) || HasAttribute<AllowAnonymousAttribute>(context))
            {
                return;
            }

            operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse
            {
                Description = "Unauthorized"
            });

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

        private static bool HasAttribute<T>(OperationFilterContext context)
        {
            return context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<T>().Any() ||
              context.MethodInfo.GetCustomAttributes(true).OfType<T>().Any();
        }
    }
}
