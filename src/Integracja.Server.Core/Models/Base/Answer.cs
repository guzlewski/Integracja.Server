using System.Collections.Generic;

namespace Integracja.Server.Core.Models.Base
{
    public class Answer : Entity
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}