using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class ConflictException : ApiException
    {
        public ConflictException(string details = null) : base(StatusCodes.Status409Conflict, details)
        {

        }
    }
}
