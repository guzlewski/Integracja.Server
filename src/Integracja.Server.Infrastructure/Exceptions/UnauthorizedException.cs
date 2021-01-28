namespace Integracja.Server.Infrastructure.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string details = null) : base(401, details)
        {

        }
    }
}
