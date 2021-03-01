namespace Integracja.Server.Infrastructure.Models
{
    public class DetailGamemodeDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public int? TimeForFullQuiz { get; set; }
        public int? TimeForOneQuestion { get; set; }
        public int? NumberOfLives { get; set; }
    }
}
