using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class UnsupportedMediaTypeException : ApiException
    {
        public UnsupportedMediaTypeException(string message = null) : base(StatusCodes.Status415UnsupportedMediaType, message)
        {
        }
    }
}
