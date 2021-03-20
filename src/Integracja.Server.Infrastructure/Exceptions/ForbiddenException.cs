using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    class ForbiddenException : ApiException
    {
        public ForbiddenException(string message = null) : base(StatusCodes.Status403Forbidden, message)
        {
        }
    }
}
