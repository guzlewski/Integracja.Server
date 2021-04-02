using System;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; }

        public ApiException(int code, string message) : base(message)
        {
            StatusCode = code;
        }

        public ApiException(int code, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = code;
        }
    }
}
