using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.Models
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int OwnerId { get; set; }
        public bool IsPublic { get; set; }
        public string Content { get; set; }
        public float PositivePoints { get; set; }
        public float NegativePoints { get; set; }
        public QuestionScoring QuestionScoring { get; set; }
        public int AnswersCount { get; set; }
        public int CorrectAnswersCount { get; set; }
    }
}
