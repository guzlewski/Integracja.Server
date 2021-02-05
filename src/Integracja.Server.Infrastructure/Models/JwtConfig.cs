namespace Integracja.Server.Infrastructure.Models
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int TokenExpirationTime { get; set; }
    }
}
