using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class ConflictException : ApiException
    {

        public ConflictException(string message = null) : base(StatusCodes.Status409Conflict, message)
        {
        }
    }
}
