using System.Collections.Generic;
using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.Models
{
    public class DetailQuestionDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public bool IsPublic { get; set; }
        public string Content { get; set; }
        public float PositivePoints { get; set; }
        public float NegativePoints { get; set; }
        public QuestionScoring QuestionScoring { get; set; }
        public ICollection<DetailAnswerDto> Answers { get; set; }
    }
}
