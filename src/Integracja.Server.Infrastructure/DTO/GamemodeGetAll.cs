namespace Integracja.Server.Infrastructure.DTO
{
    public class GamemodeGetAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeForFullQuiz { get; set; }
        public int TimeForOneQuestion { get; set; }
        public int NumberOfLives { get; set; }
        public bool IsPublic { get; set; }
        public int GamesCount { get; set; }
        public int OwnerId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
