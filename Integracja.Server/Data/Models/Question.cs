using System;
using System.Collections.Generic;

namespace Integracja.Server.Data.Models
{
    public enum QuestionScoringType
    {
        ScorePerGoodAnswer,
        ScoreIfFullyCorrect
    }

    public class Question
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDeleted { get; set; }

        public int AnswersCount { get; set; }

        public int CorrectAnswersCount { get; set; }

        public float PositivePoints { get; set; }

        public float NegativePoints { get; set; }

        public QuestionScoringType QuestionScoringType { get; set; }

        public byte[] RowVersion { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }

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
