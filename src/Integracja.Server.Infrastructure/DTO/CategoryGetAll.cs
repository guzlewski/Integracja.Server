namespace Integracja.Server.Infrastructure.DTO
{
    public class CategoryGetAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public int QuestionsCount { get; set; }
        public int OwnerId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
