using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.Models
{
    public class GameUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public GameUserState GameUserState { get; set; }
    }
}
