using System;
using System.Text.Json;
using System.Threading.Tasks;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Integracja.Server.Api.Utilities
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                _logger.LogInformation(dae, $"{nameof(DetailedApiException)}{Environment.NewLine}StatusCode: {dae.StatusCode}{Environment.NewLine}ErrorCode: {(int)dae.ErrorCode} {dae.ErrorCode}");
            }
            catch (ApiException ae)
            {
                var response = new
                {
                    ae.Message,
                    ae.StatusCode
                };

                await WriteResponse(response, ae, httpContext);
                _logger.LogInformation(ae, $"{nameof(ApiException)}{Environment.NewLine}StatusCode: {ae.StatusCode}");
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
