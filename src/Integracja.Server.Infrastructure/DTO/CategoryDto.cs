namespace Integracja.Server.Infrastructure.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionsCount { get; set; }
        public bool IsPublic { get; set; }
        public int AuthorId { get; set; }
        public string AuthorNickname { get; set; }
    }
}
