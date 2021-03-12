using System;

namespace Integracja.Server.Infrastructure.Models
{
    public class DetailUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfileThumbnail { get; set; }
        public DateTime ValidTo { get; set; }
        public string Token { get; set; }
    }
}
