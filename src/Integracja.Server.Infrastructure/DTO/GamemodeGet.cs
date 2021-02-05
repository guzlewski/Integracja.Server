namespace Integracja.Server.Infrastructure.DTO
{
    public class GamemodeGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeForFullQuiz { get; set; }
        public int TimeForOneQuestion { get; set; }
        public int NumberOfLives { get; set; }
        public bool IsPublic { get; set; }
        // TO DO List of games
        public int OwnerId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
