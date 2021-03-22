using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class PayloadTooLargeException : ApiException
    {
        public PayloadTooLargeException(string details = null) : base(StatusCodes.Status413PayloadTooLarge, details)
        {

        }
    }
}
