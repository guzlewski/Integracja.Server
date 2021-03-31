namespace Integracja.Server.Infrastructure.Models
{
    public class GameUserScoreDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ProfileThumbnail { get; set; }
        public float GameScore { get; set; }
    }
}
