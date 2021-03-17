using System;

namespace Integracja.Server.Infrastructure.Models
{
    public class GameUserQuestionDto
    {
        public bool GameOver { get; set; }
        public float? QuestionScore { get; set; }
        public DateTimeOffset? QuestionDownloadTime { get; set; }
        public DateTimeOffset? QuestionAnswerTime { get; set; }
        public DetailQuestionDto<DetailAnswerDto> Question { get; set; }
    }
}
