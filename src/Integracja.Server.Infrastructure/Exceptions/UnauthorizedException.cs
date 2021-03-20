using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message = null) : base(StatusCodes.Status401Unauthorized, message)
        {
        }
    }
}
