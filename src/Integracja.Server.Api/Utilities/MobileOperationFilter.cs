using System.Linq;
using Integracja.Server.Api.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Integracja.Server.Api.Utilities
{
    public class MobileOperationFilter : IOperationFilter
    {
        public static readonly OpenApiTag MobileTag = new OpenApiTag
        {
            Name = "Mobile",
            Description = "Endpoints for use in mobile app."
        };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.MethodInfo.GetCustomAttributes(true).OfType<MobileAttribute>().Any() &&
                !context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<MobileAttribute>().Any())
            {
                return;
            }

            operation.Tags.Add(MobileTag);
        }
    }
}
