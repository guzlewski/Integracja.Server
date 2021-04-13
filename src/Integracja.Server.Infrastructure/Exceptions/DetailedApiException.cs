using Integracja.Server.Infrastructure.Enums;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public abstract class DetailedApiException : ApiException
    {
        public ErrorCode ErrorCode { get; }

        public DetailedApiException(ErrorCode errorCode, int statusCode, string message) : base(statusCode, message)
        {
            ErrorCode = errorCode;
        }
    }
}
