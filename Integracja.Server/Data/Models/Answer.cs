using System;
using System.Collections.Generic;

namespace Integracja.Server.Data.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public byte[] RowVersion { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}
