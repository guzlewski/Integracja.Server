using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class PayloadTooLargeException : ApiException
    {
        public PayloadTooLargeException(string message = null) : base(StatusCodes.Status413PayloadTooLarge, message)
        {
        }
    }
}
