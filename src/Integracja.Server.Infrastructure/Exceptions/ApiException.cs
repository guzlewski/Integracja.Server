using System;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Details { get; set; }

        public ApiException(int code, string details)
        {
            StatusCode = code;
            Details = details;
        }
    }
}
