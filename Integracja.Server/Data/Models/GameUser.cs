using System;
using System.Collections.Generic;

namespace Integracja.Server.Data.Models
{
    public class GameUser
    {
        public float? GameScore { get; set; }

        public DateTimeOffset? GameStartTime { get; set; }

        public DateTimeOffset? GameEndTime { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<GameUserQuestion> GameUserQuestions { get; set; }

        public ICollection<GameUserQuestionAnswer> GameUserQuestionAnswers { get; set; }
    }
}
