using System.Collections.Generic;

namespace Integracja.Server.Data.Models
{
    public class GameQuestion
    {
        public float? OverridePositivePoints { get; set; }

        public float? OverrideNegativePoints { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}
