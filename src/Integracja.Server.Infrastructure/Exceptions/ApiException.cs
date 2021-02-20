using System;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Details { get; set; }

        public ApiException(int code, string details = null)
        {
            StatusCode = code;
            Details = details;
        }
    }
}
