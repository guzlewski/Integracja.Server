using System.Collections.Generic;

namespace Integracja.Server.Infrastructure.DTO
{
    public class QuestionDetailsDto : QuestionDto
    {
        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
