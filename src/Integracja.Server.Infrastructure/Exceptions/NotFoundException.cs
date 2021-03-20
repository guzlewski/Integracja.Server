using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message = null) : base(StatusCodes.Status404NotFound, message)
        {
        }
    }
}
