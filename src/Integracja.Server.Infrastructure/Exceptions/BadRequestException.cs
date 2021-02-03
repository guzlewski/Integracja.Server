using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string details = null) : base(StatusCodes.Status400BadRequest, details)
        {

        }
    }
}
