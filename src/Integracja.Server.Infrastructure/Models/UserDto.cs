using System;

namespace Integracja.Server.Infrastructure.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Picture { get; set; }
        public DateTime ValidTo { get; set; }
        public string Token { get; set; }
    }
}
