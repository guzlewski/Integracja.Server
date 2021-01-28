using System;

namespace Integracja.Server.Infrastructure.DTO
{
    public class TokenDTO
    {
        public DateTime ExpireOnDate { get; set; }
        public long ExpiryIn { get; set; }
        public string Token { get; set; }
    }
}
