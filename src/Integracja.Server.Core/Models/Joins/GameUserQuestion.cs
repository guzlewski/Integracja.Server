using System;
using System.Collections.Generic;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Identity;

namespace Integracja.Server.Core.Models.Joins
{
    public class GameUserQuestion
    {
        public float? QuestionScore { get; set; }
        public bool IsAnswered { get; set; }
        public DateTimeOffset? QuestionDownloadTime { get; set; }
        public DateTimeOffset? QuestionAnswerTime { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public GameUser GameUser { get; set; }
        public GameQuestion GameQuestion { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}