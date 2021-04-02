using System;
using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class UnprocessableEntityException : ApiException
    {
        public UnprocessableEntityException(string message = null) : base(StatusCodes.Status422UnprocessableEntity, message)
        {
        }

        public UnprocessableEntityException(string message = null, Exception innerException = null) : base(StatusCodes.Status422UnprocessableEntity, message, innerException)
        {
        }
    }
}
