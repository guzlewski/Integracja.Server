namespace Integracja.Server.Infrastructure.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public int QuestionsCount { get; set; }
    }
}
