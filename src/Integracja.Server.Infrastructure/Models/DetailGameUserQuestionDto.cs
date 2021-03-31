using System;
using System.Collections.Generic;

namespace Integracja.Server.Infrastructure.Models
{
    public class DetailGameUserQuestionDto
    {
        public float? QuestionScore { get; set; }
        public bool IsAnswered { get; set; }
        public DateTimeOffset? QuestionDownloadTime { get; set; }
        public DateTimeOffset? QuestionAnswerTime { get; set; }
        public DetailQuestionDto<DetailAnswerDto> Question { get; set; }
        public IEnumerable<int> SelectedAnswers { get; set; }
    }
}
