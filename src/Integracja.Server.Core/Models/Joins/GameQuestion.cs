using System.Collections.Generic;
using Integracja.Server.Core.Models.Base;

namespace Integracja.Server.Core.Models.Joins
{
    public class GameQuestion
    {
        public int Index { get; set; }
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