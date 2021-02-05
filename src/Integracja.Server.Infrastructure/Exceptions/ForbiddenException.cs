using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    class ForbiddenException : ApiException
    {
        public ForbiddenException(string details = null) : base(StatusCodes.Status403Forbidden, details)
        {

        }
    }
}
