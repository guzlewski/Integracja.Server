namespace Integracja.Server.Infrastructure.Models
{
    public class ApiError
    {
        public int StatusCode { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
