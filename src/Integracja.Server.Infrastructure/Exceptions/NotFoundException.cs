namespace Integracja.Server.Infrastructure.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string details = null) : base(404, details)
        {

        }
    }
}
