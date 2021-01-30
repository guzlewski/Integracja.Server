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

        private Task HandleExceptionAsync(HttpContext context, ApiException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            if (exception.Details != null)
            {
                return context.Response.WriteAsync(JsonSerializer.Serialize(new { exception.Details }));
            }

            return Task.CompletedTask;
        }
    }
}
