using Integracja.Server.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class ConflictException : DetailedApiException
    {
        public ConflictException(ErrorCode errorCode, string message = null) : base(errorCode, StatusCodes.Status409Conflict, message)
        {
        }
    }
}
