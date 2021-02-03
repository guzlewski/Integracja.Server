namespace Integracja.Server.Infrastructure.DTO
{
    public class QuestionDto : QuestionShortDto
    {
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}
