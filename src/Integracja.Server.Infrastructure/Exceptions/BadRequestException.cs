using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message = null) : base(StatusCodes.Status400BadRequest, message)
        {
        }
    }
}
