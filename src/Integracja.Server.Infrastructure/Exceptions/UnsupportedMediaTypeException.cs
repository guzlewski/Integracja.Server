using Microsoft.AspNetCore.Http;

namespace Integracja.Server.Infrastructure.Exceptions
{
    public class UnsupportedMediaTypeException : ApiException
    {
        public UnsupportedMediaTypeException(string details = null) : base(StatusCodes.Status415UnsupportedMediaType, details)
        {

        }
    }
}
