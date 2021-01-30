using System;

namespace Integracja.Server.Infrastructure.DTO
{
    public class TokenDto
    {
        public DateTime ExpireOnDate { get; set; }
        public long ExpiryIn { get; set; }
        public string Token { get; set; }
    }
}
