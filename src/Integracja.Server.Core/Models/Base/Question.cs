using System;
using System.Collections.Generic;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Core.Models.Interfaces;
using Integracja.Server.Core.Models.Joins;

namespace Integracja.Server.Core.Models.Base
{
    public class Question : IEntity, ISoftDeleteable, IHideable
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public int RowVersion { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; }

        public string Content { get; set; }
        public float PositivePoints { get; set; }
        public float NegativePoints { get; set; }
        public QuestionScoring QuestionScoring { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<GameQuestion> GameQuestions { get; set; }
        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }
        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}