using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string details = null) : base(StatusCodes.Status404NotFound, details)
        {

        }
    }
}
