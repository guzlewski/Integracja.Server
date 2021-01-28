using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.DTO
{
    public class LoginDto
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
