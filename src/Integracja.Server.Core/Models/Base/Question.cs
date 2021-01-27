using System.Collections.Generic;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Models.Base
{
    public enum QuestionScoringType
    {
        ScorePerGoodAnswer,
        ScoreIfFullyCorrect
    }

    public class Question : SoftDeleteableEntity
    {
        public string Content { get; set; }
        public int AnswersCount { get; set; }
        public int CorrectAnswersCount { get; set; }
        public float PositivePoints { get; set; }
        public float NegativePoints { get; set; }
        public QuestionScoringType QuestionScoringType { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<GameQuestion> GameQuestions { get; set; }
        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }
        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}