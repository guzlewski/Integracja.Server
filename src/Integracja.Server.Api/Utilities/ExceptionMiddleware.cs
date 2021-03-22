using System.Text.Json;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Api.Utilities
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
            catch (DetailedApiException dae)
            {
                var apiError = new ApiError
                {
                    ErrorCode = (int)dae.ErrorCode,
                    Message = dae.Message,
                    StatusCode = dae.StatusCode
                };

                await WriteResponse(apiError, dae, httpContext);
            }
            catch (ApiException ae)
            {
                var response = new
                {       
                    ae.Message,
                    ae.StatusCode
                };

                await WriteResponse(response, ae, httpContext);
            }
        }

        private static async Task WriteResponse<T>(T response, ApiException apiException, HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = apiException.StatusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
