using System;
using System.Collections.Generic;
using Integracja.Server.Core.Models.Interfaces;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Models.Base
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public int RowVersion { get; set; }

        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}