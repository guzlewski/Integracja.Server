using System;

namespace Integracja.Server.Infrastructure.Models
{
    public class GameUserQuestionDto<T>
    {
        public bool GameOver { get; set; }
        public float? QuestionScore { get; set; }
        public DateTimeOffset? QuestionDownloadTime { get; set; }
        public DateTimeOffset? QuestionAnswerTime { get; set; }
        public DetailQuestionDto<T> Question { get; set; }
    }
}
