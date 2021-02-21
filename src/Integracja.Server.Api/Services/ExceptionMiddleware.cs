using System.Text.Json;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Api.Services
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ApiException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, ApiException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                exception.StatusCode,
                exception.Details
            }));
        }
    }
}
