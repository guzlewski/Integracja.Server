using System.Collections.Generic;
using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.DTO
{
    public class QuestionGet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public float PositivePoints { get; set; }
        public float NegativePoints { get; set; }
        public QuestionScoring QuestionScoring { get; set; }
        public bool IsPublic { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
