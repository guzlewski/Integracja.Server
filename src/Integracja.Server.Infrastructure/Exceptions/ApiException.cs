using System;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException(int code, string message) : base(message)
        {
            StatusCode = code;
        }
    }
}
